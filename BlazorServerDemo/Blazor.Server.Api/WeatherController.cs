using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Api;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService weatherService;
    private readonly ILogger<WeatherController> logger;
    public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
    {
        this.weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        this.logger         = logger         ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date">yyyyMMdd</param>
    /// <returns></returns>
    [HttpGet("forecasts")]
    public async Task<IActionResult> GetForecasts(string? date)
    {
        logger.LogInformation(nameof(GetForecasts));

        DateTime startDate = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(date))
        {
            startDate = DateTime.ParseExact(date, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None);
        }

        WeatherForecast[] forecasts = await weatherService.GetWeatherForecastsAsync(startDate);
        return Ok(forecasts);
    }
}
