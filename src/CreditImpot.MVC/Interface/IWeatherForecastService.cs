using CreditImpot.MVC.Models;

namespace CreditImpot.MVC.Interface
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> ObtenirTout();

      
    }
}
