using Blazor.Shared.Models;
using Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Api;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ILogger<UserController> logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(User));
        this.logger      = logger      ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        logger.LogInformation(nameof(GetCurrent));
        return Ok(await userService.GetCurrentUserAsync());
    }
}
