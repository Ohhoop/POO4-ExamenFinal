using CreditImpot.API.Interfaces;
using CreditImpot.API.Models;

namespace CreditImpot.API.Services
{
    public class CalculCredit : ICalculCredit
    {
        public decimal CalculerCredit(DemandeCredit demande)
        {
            decimal salaireFamilial = demande.SalaireMere + demande.SalairePere;
            
            decimal taux = 0;
            
            if (demande.MontantFrais <= 2300m)
            {
                return 0m;
            }
            if (salaireFamilial > 0 && salaireFamilial <= 22000) { taux = 0.78m; }
            else if (salaireFamilial > 22000 && salaireFamilial <= 40000) { taux = 0.75m; }
            else if (salaireFamilial > 40000 && salaireFamilial <= 60000) { taux = 0.72m; }
            else if (salaireFamilial > 60000) { taux = 75m; }
            
           return (demande.MontantFrais - 2300m) * taux;
            
        }
    }
}
