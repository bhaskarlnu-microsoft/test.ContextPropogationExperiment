using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace secondAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SecondController : ControllerBase
{
    private readonly ILogger<SecondController> _logger;
    private readonly ActivitySource _activitySource;

    public SecondController(ILogger<SecondController> logger)
    {
        _logger = logger;
        _activitySource = new ActivitySource("SampleActivitySourceTwo");
    }

    [HttpGet(Name = "GetSecond")]
    public IActionResult Get()
    {
        using var activity = _activitySource.StartActivity("secondApi");
        activity?.SetTag("WeatherForecast", "WeatherForecast");

        return Ok(new { message = "Hello from Api2" });
    }
}
