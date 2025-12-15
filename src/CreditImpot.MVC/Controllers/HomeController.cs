using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CreditImpot.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWeatherForecastService _weatherForecastService;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Weather()
        {
            return View(await _weatherForecastService.ObtenirTout());
        }
        
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}