using Messaging;
using Microsoft.AspNetCore.Mvc;

namespace LearnRabbitMQ.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMassageBusService _service;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMassageBusService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var x = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
        _service.Publish(x);        

        return x;
        
    }
}
