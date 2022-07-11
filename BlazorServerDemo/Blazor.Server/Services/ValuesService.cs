using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Blazor.Server.Interfaces;

namespace Blazor.Server.Services;

public class ValuesService : IValuesService
{
    private readonly IRepository  repository;
    private readonly IUserService userService;
    private readonly ILogger      logger;

    public ValuesService(IRepository repository, IUserService userService, ILogger<ValuesService> logger)
    {
        this.repository  = repository  ?? throw new ArgumentNullException(nameof(repository));
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.logger      = logger      ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        logger.LogInformation(nameof(GetWeatherForecastsAsync));
        return repository.GetForecastsAsync(startDate);
    }

    public Task<User> GetLoggedInUser()
    {
        logger.LogInformation(nameof(GetLoggedInUser));
        return Task.FromResult(userService.CurrentUser);
    }
}
