using CreditImpot.MVC.Models;

namespace CreditImpot.MVC.Interface
{
    public interface IFraisGardeService
    {
        public Task<HttpResponseMessage> Ajouter(DemandeCredit demandeCredit);
        public Task<IEnumerable<DemandeCreditFraisGarde>> ObtenirSelonNAs(string? NAS);

        public Task Supprimer(int id);
    }
}
