using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static List<WeatherForecast> forecasts = new()
        {
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            },
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            },
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return forecasts;
        }

        [HttpGet("today", Name = "GetTodayForecast")]
        public WeatherForecast GetToday()
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPut("{index}")]
        public IActionResult UpdateForecast(int index, [FromBody] WeatherForecast updateForecast)
        {
            if (index < 0 || index >= forecasts.Count)
                return NotFound("Not found");

            forecasts[index] = updateForecast;
            return Ok(forecasts[index]);
        }

        [HttpPost]
        public void AddForecast([FromBody] WeatherForecast newForecast)
        {
            forecasts.Add(newForecast);
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteForecast(int index)
        {
            if (index < 0 || index >= forecasts.Count)
                return NotFound("Not found");

            forecasts.RemoveAt(index);
            return NoContent();
        }
    }
}
