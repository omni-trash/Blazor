namespace Blazor.Server.Interfaces;

using Shared.Models;

public interface IRepository
{
    Task<WeatherForecast[]> GetForecastsAsync(DateTime startDate);
}