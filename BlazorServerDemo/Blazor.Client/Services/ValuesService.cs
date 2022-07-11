using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using System.Net.Http.Json;

namespace Blazor.Client.Services;

public class ValuesService : IValuesService
{
    private readonly HttpClient http;
    private readonly ILogger    logger;

    public ValuesService(HttpClient http, ILogger<ValuesService> logger)
    {
        this.http   = http   ?? throw new ArgumentNullException(nameof(http));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        logger.LogInformation(nameof(GetWeatherForecastsAsync));
        return (await http.GetFromJsonAsync<WeatherForecast[]>("api/values/forecasts?date=" + startDate))!;
    }

    public async Task<User> GetLoggedInUser()
    {
        logger.LogInformation(nameof(GetLoggedInUser));
        return (await http.GetFromJsonAsync<User>("api/values/user"))!;
    }
}
