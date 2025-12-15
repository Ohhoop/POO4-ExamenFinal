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

        [Fact]
        public void CalculerCredit_MontantFrais2300_RetourneZero()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 2300,
                SalaireMere = 15000,
                SalairePere = 15000
            };

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(0);
        }

        [Fact]
        public void CalculerCredit_MontantFrais2301_RetourneCredit()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 2301,
                SalaireMere = 10000,
                SalairePere = 10000
            };

            decimal creditAttendu = (2301 - 2300) * 0.80m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }

        [Fact]
        public void CalculerCredit_RevenuFamilial21999_AppliquerTaux80Pourcent()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 5000,
                SalaireMere = 11000,
                SalairePere = 10999
            };

            decimal creditAttendu = (5000 - 2300) * 0.80m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }

        [Fact]
        public void CalculerCredit_RevenuFamilial22000_AppliquerTaux75Pourcent()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 3000,
                SalaireMere = 11000,
                SalairePere = 11000
            };

            decimal creditAttendu = (3000 - 2300) * 0.75m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }

        [Fact]
        public void CalculerCredit_RevenuFamilial39999_AppliquerTaux75Pourcent()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 4000,
                SalaireMere = 20000,
                SalairePere = 19999
            };

            decimal creditAttendu = (4000 - 2300) * 0.75m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }

        [Fact]
        public void CalculerCredit_RevenuFamilial40000_AppliquerTaux72Pourcent()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 3000,
                SalaireMere = 20000,
                SalairePere = 20000
            };

            decimal creditAttendu = (3000 - 2300) * 0.72m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }

        [Fact]
        public void CalculerCredit_RevenuFamilial59999_AppliquerTaux72Pourcent()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 4000,
                SalaireMere = 30000,
                SalairePere = 29999
            };

            decimal creditAttendu = (4000 - 2300) * 0.72m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }

        [Fact]
        public void CalculerCredit_RevenuFamilial60000_AppliquerTaux60Pourcent()
        {
            //Étant donné
            var demande = new DemandeCredit
            {
                MontantFrais = 3000,
                SalaireMere = 30000,
                SalairePere = 30000
            };

            decimal creditAttendu = (3000 - 2300) * 0.60m;

            //Quand
            var resultat = _calculCredit.CalculerCredit(demande);

            //Alors
            resultat.Should().Be(creditAttendu);
        }
    }
}
