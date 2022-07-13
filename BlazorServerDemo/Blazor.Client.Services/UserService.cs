using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Blazor.Client.Services;

public class UserService : IUserService
{
    private readonly HttpClient http;
    private readonly ILogger logger;

    public UserService(HttpClient http, ILogger<UserService> logger)
    {
        this.http   = http   ?? throw new ArgumentNullException(nameof(http));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public async Task<User> GetCurrentUserAsync()
    {
        logger.LogInformation(nameof(GetCurrentUserAsync));
        return (await http.GetFromJsonAsync<User>("api/User/current"))!;
    }
}
