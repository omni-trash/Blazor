using Blazor.Shared.Models;

namespace Blazor.Server.Shared.Interfaces;

public interface IUserService
{
    User CurrentUser { get; }
}
