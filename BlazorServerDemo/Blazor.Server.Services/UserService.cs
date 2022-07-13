using Blazor.Shared.Interfaces;
using Blazor.Shared.Models;
using System.Security.Principal;

namespace Blazor.Server.Services;

public class UserService : IUserService
{
    public User CurrentUser { get; } = new User();

    public UserService(IPrincipal? principal)
    {
        if (principal?.Identity?.IsAuthenticated == true)
        {
            CurrentUser = new User
            {
                IsAuthenticated = true,
                LoginName       = Utils.WindowsUserUtil.GetUserLoginName(principal),
                DisplayName     = Utils.WindowsUserUtil.GetUserDisplayName(principal)
            };
        }
    }

    public Task<User> GetCurrentUserAsync() => Task.FromResult(CurrentUser);
}
