using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Api;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
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
        logger.LogInformation(nameof(Get));
        DateTime startDate = date ?? DateTime.Now;
        WeatherForecast[] forecasts = await values.GetWeatherForecastsAsync(startDate);
        return Ok(forecasts);
    }

    [HttpGet]
    [Route("user")]
    public async Task<IActionResult> GetLoggedInUser()
    {
        logger.LogInformation(nameof(GetLoggedInUser));
        User user = await values.GetLoggedInUser();
        return Ok(user);
    }
}
