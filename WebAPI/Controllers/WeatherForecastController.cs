using JsonDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly EntityService telegramBotPackage;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, EntityService telegramBotPackage)
        {
            _logger = logger;
            this.telegramBotPackage = telegramBotPackage;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return this.telegramBotPackage.Get<WeatherForecast>();
        }

        [HttpPost]
        public async Task<IActionResult> Add(WeatherForecast weatherForecast)
        {
            this.telegramBotPackage.Add(weatherForecast);

            return Ok();
        }
    }
}
