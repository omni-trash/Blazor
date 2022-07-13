using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Blazor.Client.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient http;
    private readonly ILogger    logger;

    public WeatherService(HttpClient http, ILogger<WeatherService> logger)
    {
        this.http   = http   ?? throw new ArgumentNullException(nameof(http));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        logger.LogInformation(nameof(GetWeatherForecastsAsync));
        return (await http.GetFromJsonAsync<WeatherForecast[]>("api/Weather/forecasts?date=" + startDate.ToString("yyyyMMdd")))!;
    }
}
