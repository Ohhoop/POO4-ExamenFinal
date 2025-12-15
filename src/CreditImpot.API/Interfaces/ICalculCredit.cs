using CreditImpot.API.Models;

namespace CreditImpot.API.Interfaces
{
    public interface ICalculCredit
    {
        public decimal CalculerCredit(DemandeCredit demande);
    }
}
