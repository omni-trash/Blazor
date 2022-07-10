using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using System.Net.Http.Json;

namespace Blazor.Client.Services;

public class ValuesService : IValuesService
{
    private HttpClient http;

    public ValuesService(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        return (await http.GetFromJsonAsync<WeatherForecast[]>("api/values/forecasts?date=" + startDate))!;
    }

    public async Task<User> GetLoggedInUser()
    {
        return (await http.GetFromJsonAsync<User>("api/values/user"))!;
    }
}
