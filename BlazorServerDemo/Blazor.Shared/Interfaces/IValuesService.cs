using Blazor.Shared.Models;
namespace Blazor.Shared.Interfaces;

public interface IValuesService
{
    Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate);
    Task<User> GetLoggedInUser();
}
