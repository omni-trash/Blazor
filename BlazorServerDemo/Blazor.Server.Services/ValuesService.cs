using Blazor.Server.Shared.Interfaces;
using Blazor.Shared.Interfaces;
using Blazor.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Blazor.Server.Services;

public class ValuesService : IValuesService
{
    private readonly IWeatherRepository weatherRepository;
    private readonly IUserService userService;
    private readonly ILogger logger;

    public ValuesService(IWeatherRepository weatherRepository, IUserService userService, ILogger<ValuesService> logger)
    {
        this.weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
        this.userService       = userService       ?? throw new ArgumentNullException(nameof(userService));
        this.logger            = logger            ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        logger.LogInformation(nameof(GetWeatherForecastsAsync));
        return weatherRepository.GetForecastsAsync(startDate);
    }

    public Task<User> GetLoggedInUser()
    {
        logger.LogInformation(nameof(GetLoggedInUser));
        return Task.FromResult(userService.CurrentUser);
    }
}
