using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AnotherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return new[]
            {
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = 25,
                    Summary = "Warm"
                }
            };
        }
    }
}
