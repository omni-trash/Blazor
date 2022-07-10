using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ValuesController : ControllerBase
{
    private readonly IValuesService values;
    private readonly ILogger<ValuesController> logger;

    public ValuesController(IValuesService values, ILogger<ValuesController> logger)
    {
        this.values = values ?? throw new ArgumentNullException(nameof(values));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Route("forecasts")]
    public async Task<IActionResult> Get(DateTime? date)
    {
        logger.LogTrace("Get called");
        DateTime startDate = date ?? DateTime.Now;
        WeatherForecast[] forecasts = await values.GetWeatherForecastsAsync(startDate);
        return Ok(forecasts);
    }

    [HttpGet]
    [Route("user")]
    public async Task<IActionResult> GetLoggedInUser()
    {
        logger.LogTrace("GetLoggedInUser called");
        User user = await values.GetLoggedInUser();
        return Ok(user);
    }
}
