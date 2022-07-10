using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Blazor.Server.Interfaces;

namespace Blazor.Server.Services;

public class ValuesService : IValuesService
{
    private readonly IRepository  repository;
    private readonly IUserService userService;

    public ValuesService(IRepository repository, IUserService userService)
    {
        this.repository  = repository  ?? throw new ArgumentNullException(nameof(repository));
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public Task<WeatherForecast[]> GetWeatherForecastsAsync(DateTime startDate)
    {
        return repository.GetForecastsAsync(startDate);
    }

    public Task<User> GetLoggedInUser()
    {
        return Task.FromResult(userService.CurrentUser);
    }
}
