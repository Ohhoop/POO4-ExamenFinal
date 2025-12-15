using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace CreditImpot.MVC.Services
{
    public class FraisGardeServiceProxy : IFraisGardeService
    {
        private readonly HttpClient _httpClient;
        private const string _ApiURL = "api/FraisGarde/";
        public FraisGardeServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> Ajouter(DemandeCredit demandeCredit)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(demandeCredit), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(_ApiURL, content);
        }

        public async Task<IEnumerable<DemandeCreditFraisGarde>> ObtenirSelonNAs(string? NAS)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<DemandeCreditFraisGarde>>(_ApiURL + "?NAS=" + NAS);
        }

        public async Task Supprimer(int id)
        {
            await _httpClient.DeleteAsync(_ApiURL + id);
        }
    }
}
