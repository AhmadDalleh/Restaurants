namespace Restaurants.API.Controllers
{

    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int? reslutsnumber, int? minmmtemp, int? maximumtemp);
    }

    public class WeatherForecastService: IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> Get(int? reslutsnumber,int? minmmtemp,int? maximumtemp)
        {
            
            return Enumerable.Range(1, reslutsnumber??=5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minmmtemp??=-5, maximumtemp??=55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}