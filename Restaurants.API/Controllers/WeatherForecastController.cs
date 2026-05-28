using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Restaurants.API.Controllers
{


    public class TempreatureRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]

    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        [Route("example")]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _weatherForecastService.Get(null,null,null);
            return result;
        }

        [HttpGet]
        [Route("{take}/currentDay")]
        public IActionResult Get([FromQuery] int max, [FromRoute] int take)
        {
            var result = _weatherForecastService.Get(null,null,null).First();

            //Response.StatusCode = 400;
            //  return StatusCode(400, result);
            return NotFound(result);
        }
        [HttpGet]
        [Route("{reslutNumber}")]
        public IActionResult Get([FromQuery] int min, [FromQuery] int max, [FromRoute] int reslutNumber)
        {
            var result = _weatherForecastService.Get(reslutNumber, min, max);
            //Response.StatusCode = 400;
            //  return StatusCode(400, result);
            return Ok(result);
        }


        [HttpPost]
        [Route("genrate")]
        public IActionResult Genrate([FromQuery] int reslutsNumber, [FromBody] TempreatureRange range)
        {

            if(reslutsNumber <= 0 || range.Max < range.Min)
            {
                return BadRequest("the resluts number should be greater than 0 and the max tempreature should be greater than the min tempreature");
            }
            var result = _weatherForecastService.Get(reslutsNumber, range.Min, range.Max);
            return Ok(result);
        }
             
        


        [HttpPost]
        public string Hello([FromBody] string name)
        {
            return $"Hello {name}";
        }
    }
}
