using Blazor.Shared.Models;

namespace Blazor.Shared.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate);
}
