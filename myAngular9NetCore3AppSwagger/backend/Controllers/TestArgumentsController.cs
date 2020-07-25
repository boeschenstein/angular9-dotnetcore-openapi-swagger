using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestArgumentsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public TestArgumentsController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int rangeStart, int rangeEnd)
        {
            // curl -X GET "https://localhost:5001/TestArguments?rangeStart=1&rangeEnd=4" -H "accept: text/plain"

            if (rangeStart == 0) rangeStart = 1;
            if (rangeEnd == 0) rangeEnd = 5;

            var rng = new Random();
            return Enumerable.Range(rangeStart, rangeEnd).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IEnumerable<WeatherForecast> Post(int rangeStart, int rangeEnd)
        {
            if (rangeStart == 0) rangeStart = 1;
            if (rangeEnd == 0) rangeEnd = 5;

            var rng = new Random();
            return Enumerable.Range(rangeStart, rangeEnd).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}