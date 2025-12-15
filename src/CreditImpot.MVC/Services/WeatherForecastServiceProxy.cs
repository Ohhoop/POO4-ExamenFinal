using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Models;

namespace CreditImpot.MVC.Services
{
    public class WeatherForecastServiceProxy : IWeatherForecastService
    {
        private readonly HttpClient _httpClient;
        private const string _weatherApiUrl = "weatherforecast/";

        public WeatherForecastServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<WeatherForecast>> ObtenirTout()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>(_weatherApiUrl);
        }
        
    }
}
