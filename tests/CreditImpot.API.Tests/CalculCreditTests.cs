using CreditImpot.API.Models;
using CreditImpot.API.Services;
using FluentAssertions;

namespace CreditImpot.API.Tests
{
    public class CalculCreditTests
    {
        private readonly CalculCredit _calculCredit;

        public CalculCreditTests()
        {
            _calculCredit = new CalculCredit();
        }

        [Theory]
        [InlineData(2300, 30000, 0)]
        [InlineData(2200, 30000, 0)]
        [InlineData(0, 30000, 0)]
        public void CalculerCredit_MontantFraisInferieurOuEgal2300_RetourneZero(int montantFrais, int salaireFamilial, decimal creditAttendu)
        {
            // Arrange
            var demande = new DemandeCredit
            {
                MontantFrais = montantFrais,
                SalaireMere = salaireFamilial / 2,
                SalairePere = salaireFamilial / 2
            };

            // Act
            var resultat = _calculCredit.CalculerCredit(demande);

            // Assert
            resultat.Should().Be(creditAttendu);
        }

        [Theory]
        [InlineData(3000, 10000, 0.78)]
        [InlineData(5000, 22000, 0.78)]
        public void CalculerCredit_RevenuFamilial0A22000_AppliquerTaux78Pourcent(int montantFrais, int salaireFamilial, decimal tauxAttendu)
        {
            // Arrange
            var demande = new DemandeCredit
            {
                MontantFrais = montantFrais,
                SalaireMere = salaireFamilial / 2,
                SalairePere = salaireFamilial / 2
            };

            decimal creditAttendu = (montantFrais - 2300) * tauxAttendu;

            // Act
            var resultat = _calculCredit.CalculerCredit(demande);

            // Assert
            resultat.Should().Be(creditAttendu);
        }

        [Theory]
        [InlineData(3000, 22001, 0.75)]
        [InlineData(5000, 30000, 0.75)]
        [InlineData(4000, 40000, 0.75)]
        public void CalculerCredit_RevenuFamilial22001A40000_AppliquerTaux75Pourcent(int montantFrais, int salaireFamilial, decimal tauxAttendu)
        {
            // Arrange
            var demande = new DemandeCredit
            {
                MontantFrais = montantFrais,
                SalaireMere = salaireFamilial / 2,
                SalairePere = salaireFamilial / 2
            };

            decimal creditAttendu = (montantFrais - 2300) * tauxAttendu;

            // Act
            var resultat = _calculCredit.CalculerCredit(demande);

            // Assert
            resultat.Should().Be(creditAttendu);
        }

        [Theory]
        [InlineData(3000, 40001, 0.72)]
        [InlineData(5000, 50000, 0.72)]
        [InlineData(4000, 60000, 0.72)]
        public void CalculerCredit_RevenuFamilial40001A60000_AppliquerTaux72Pourcent(int montantFrais, int salaireFamilial, decimal tauxAttendu)
        {
            // Arrange
            var demande = new DemandeCredit
            {
                MontantFrais = montantFrais,
                SalaireMere = salaireFamilial / 2,
                SalairePere = salaireFamilial / 2
            };

            decimal creditAttendu = (montantFrais - 2300) * tauxAttendu;

            // Act
            var resultat = _calculCredit.CalculerCredit(demande);

            // Assert
            resultat.Should().Be(creditAttendu);
        }

        [Theory]
        [InlineData(3000, 60001, 75)]
        [InlineData(5000, 70000, 75)]
        [InlineData(4000, 100000, 75)]
        public void CalculerCredit_RevenuFamilialSuperieur60000_AppliquerTaux60Pourcent(int montantFrais, int salaireFamilial, decimal tauxAttendu)
        {
            // Arrange
            var demande = new DemandeCredit
            {
                MontantFrais = montantFrais,
                SalaireMere = salaireFamilial / 2,
                SalairePere = salaireFamilial / 2
            };

            decimal creditAttendu = (montantFrais - 2300) * tauxAttendu;

            // Act
            var resultat = _calculCredit.CalculerCredit(demande);

            // Assert
            resultat.Should().Be(creditAttendu);
        }
    }
}
