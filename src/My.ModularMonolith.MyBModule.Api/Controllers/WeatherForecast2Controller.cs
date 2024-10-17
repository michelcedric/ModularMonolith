using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace My.ModularMonolith.MyBModule.Api.Controllers;

[ApiController]
[FeatureGate(Constants.Api.ModuleName)]
[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[Route("MyBModule/api/WeatherForecast2")]
public class WeatherForecast2Controller : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecast2Controller> _logger;

    public WeatherForecast2Controller(ILogger<WeatherForecast2Controller> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast2>), 200)]
    public IActionResult Get()
    {
        return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast2
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
    }
}