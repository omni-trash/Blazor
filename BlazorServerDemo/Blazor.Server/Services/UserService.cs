using Blazor.Shared.Models;
using Blazor.Server.Interfaces;
using Blazor.Server.Utils;
using System.Security.Principal;

namespace Blazor.Server.Services;

public class UserService : IUserService
{
    public User CurrentUser { get; } = new User();

    public UserService(IHttpContextAccessor contextAccessor)
    {
        IPrincipal? principal = contextAccessor.HttpContext?.User;

        if (principal?.Identity?.IsAuthenticated == true)
        {
            CurrentUser = new User
            {
                IsAuthenticated = true,
                LoginName       = WindowsUserUtil.GetUserLoginName(principal),
                DisplayName     = WindowsUserUtil.GetUserDisplayName(principal)
            };
        }
    }
}
