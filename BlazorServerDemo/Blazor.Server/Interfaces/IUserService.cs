using Blazor.Shared.Models;

namespace Blazor.Server.Interfaces;

public interface IUserService
{
    User CurrentUser { get; }
}
