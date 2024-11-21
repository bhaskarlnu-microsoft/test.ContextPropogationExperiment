using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace firstAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FirstController : ControllerBase
{
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<FirstController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ActivitySource activitySource; 

    public FirstController(ILogger<FirstController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        activitySource = new ActivitySource("SampleActivitySource");
    }

    [HttpGet(Name = "GetFirst")]
    public async Task<IActionResult> Get()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5001/second");
        var content = await response.Content.ReadAsStringAsync();

        using (var activity = activitySource.StartActivity("firstApi"))
        {
            activity?.SetTag("SampleTag", "SampleValue");
            activity?.SetTag("SampleTag2", "SampleValue2");
        }
        return Ok(new { message = "Hello from Api1", response = content });
    }
}
