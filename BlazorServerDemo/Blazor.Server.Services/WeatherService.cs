using Blazor.Server.Shared.Interfaces;
using Blazor.Shared.Interfaces;
using Blazor.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Blazor.Server.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository weatherRepository;
    private readonly ILogger logger;

    public WeatherService(IWeatherRepository weatherRepository, ILogger<WeatherService> logger)
    {
        this.weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
        this.logger            = logger            ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        logger.LogInformation(nameof(GetWeatherForecastsAsync));
        return weatherRepository.GetForecastsAsync(startDate);
    }
}
