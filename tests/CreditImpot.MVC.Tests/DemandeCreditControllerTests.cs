using CreditImpot.MVC.Controllers;
using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Models;
using FluentAssertions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace CreditImpot.MVC.Tests
{
    public class DemandeCreditControllerTests
    {
        private readonly Mock<IFraisGardeService> _mockFraisGardeService;
        private readonly Mock<IDataProtectionProvider> _mockDataProtectionProvider;
        private readonly Mock<IDataProtector> _mockDataProtector;
        private readonly DemandeCreditController _controller;

        public DemandeCreditControllerTests()
        {
            _mockFraisGardeService = new Mock<IFraisGardeService>();
            _mockDataProtectionProvider = new Mock<IDataProtectionProvider>();
            _mockDataProtector = new Mock<IDataProtector>();

            _mockDataProtectionProvider
                .Setup(x => x.CreateProtector("NASProtection"))
                .Returns(_mockDataProtector.Object);

            _controller = new DemandeCreditController(
                _mockFraisGardeService.Object,
                _mockDataProtectionProvider.Object);
        }

        [Fact]
        public async Task Create_MontantFraisInferieurOuEgal2300_APInonAppelee_MessageErreurAffiche()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                NAS = "123456789",
                NomEnfant = "Test Enfant",
                DateNaissance = DateTime.Now.AddYears(-5),
                EstAtteintDeficience = false,
                SalaireMere = 30000,
                SalairePere = 35000,
                MontantFrais = 2300
            };

            //Quand
            var result = await _controller.Create(demande);

            //Alors
            _mockFraisGardeService.Verify(x => x.Ajouter(It.IsAny<DemandeCredit>()), Times.Never);

            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().Be(demande);

            _controller.ModelState.Should().ContainKey("MontantFrais");
            _controller.ModelState["MontantFrais"].Errors[0].ErrorMessage
                .Should().Be("Le Montant des frais de garde doit être supérieur à 2 300$ pour avoir droit au crédit.");
        }

        [Fact]
        public async Task Create_APIRetourneCreated_RedirigerVersIndex()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                NAS = "123456789",
                NomEnfant = "Test Enfant",
                DateNaissance = DateTime.Now.AddYears(-5),
                EstAtteintDeficience = false,
                SalaireMere = 30000,
                SalairePere = 35000,
                MontantFrais = 3000
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.Created);

            _mockFraisGardeService
                .Setup(x => x.Ajouter(It.IsAny<DemandeCredit>()))
                .ReturnsAsync(httpResponse);

            //Quand
            var result = await _controller.Create(demande);

            //Alors
            _mockFraisGardeService.Verify(x => x.Ajouter(demande), Times.Once);

            result.Should().BeOfType<RedirectToActionResult>();
            var redirectResult = result as RedirectToActionResult;
            redirectResult.ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task Create_APIRetourneAutreQueCreated_AjouterErreurModelState()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                NAS = "123456789",
                NomEnfant = "Test Enfant",
                DateNaissance = DateTime.Now.AddYears(-5),
                EstAtteintDeficience = false,
                SalaireMere = 30000,
                SalairePere = 35000,
                MontantFrais = 3000
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

            _mockFraisGardeService
                .Setup(x => x.Ajouter(It.IsAny<DemandeCredit>()))
                .ReturnsAsync(httpResponse);

            //Quand
            var result = await _controller.Create(demande);

            //Alors
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().Be(demande);

            _controller.ModelState.Should().ContainKey("Erreur du service Web");
        }

        [Fact]
        public async Task Create_EnCasErreur_InformationsSaisiesRetourneesVue()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                NAS = "123456789",
                NomEnfant = "Test Enfant",
                DateNaissance = DateTime.Now.AddYears(-5),
                EstAtteintDeficience = false,
                SalaireMere = 30000,
                SalairePere = 35000,
                MontantFrais = 2100
            };

            //Quand
            var result = await _controller.Create(demande);

            //Alors
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().Be(demande);
            viewResult.Model.Should().BeEquivalentTo(demande);
        }
    }
}
