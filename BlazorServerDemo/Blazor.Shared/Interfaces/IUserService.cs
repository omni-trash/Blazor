using Blazor.Shared.Models;

namespace Blazor.Shared.Interfaces;

public interface IUserService
{
    Task<User> GetCurrentUserAsync();
}
