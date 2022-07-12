using Blazor.Shared.Models;

namespace Blazor.Server.Shared.Interfaces;

public interface IWeatherRepository
{
    Task<WeatherForecast[]> GetForecastsAsync(DateTime startDate);
}
