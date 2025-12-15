using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Models;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System.Text;

namespace CreditImpot.MVC.Services
{
    public class FraisGardeServiceProxy : IFraisGardeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FraisGardeServiceProxy> _logger;
        private readonly IDataProtector _dataProtector;
        private const string _ApiURL = "api/FraisGarde/";
        public FraisGardeServiceProxy(HttpClient httpClient, ILogger<FraisGardeServiceProxy> logger, IDataProtectionProvider dataProtectionProvider)
        {
            _httpClient = httpClient;
            _logger = logger;
            _dataProtector = dataProtectionProvider.CreateProtector("NASProtection");
        }
        public async Task<HttpResponseMessage> Ajouter(DemandeCredit demandeCredit)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(demandeCredit), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_ApiURL, content);

            LogHttpResponse(response);

            return response;
        }

        public async Task<IEnumerable<DemandeCreditFraisGarde>> ObtenirSelonNAs(string? NAS)
        {
            string? nasChiffre = null;
            if (!string.IsNullOrEmpty(NAS))
            {
                nasChiffre = _dataProtector.Protect(NAS);
            }

            var response = await _httpClient.GetAsync(_ApiURL + "?NAS=" + nasChiffre);

            LogHttpResponse(response);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<DemandeCreditFraisGarde>>(content);
            }

            return new List<DemandeCreditFraisGarde>();
        }

        public async Task Supprimer(int id)
        {
            var response = await _httpClient.DeleteAsync(_ApiURL + id);

            LogHttpResponse(response);
        }

        private void LogHttpResponse(HttpResponseMessage response)
        {
            int statusCode = (int)response.StatusCode;

            if (statusCode >= 200 && statusCode < 300)
            {
                _logger.LogInformation("Code HTTP {StatusCode} retourné par l'API", statusCode);
            }
            else if (statusCode >= 400 && statusCode < 500)
            {
                _logger.LogError("Code HTTP {StatusCode} retourné par l'API", statusCode);
            }
            else if (statusCode >= 500)
            {
                _logger.LogCritical("Code HTTP {StatusCode} retourné par l'API", statusCode);
                throw new HttpRequestException("Erreur grave dans l'API.");
            }
        }
    }
}
