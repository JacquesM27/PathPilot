using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathPilot.Modules.Users.Core.DTO;
using PathPilot.Modules.Users.Core.Services;
using PathPilot.Shared.Abstractions.Auth;
using PathPilot.Shared.Abstractions.Contexts;

namespace PathPilot.Modules.Users.Api.Controllers;

internal sealed class AccountController(
    IIdentityService identityService,
    IContext context
    ) : BaseController
{
    private const string Policy = "users";
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<AccountDto?>> GetAsync()
        => OkOrNotFound(await identityService.GetAsync(context.Identity.Id));

    [HttpPost("sign-up-user")]
    public async Task<ActionResult> SignUpUserAsync(SignUpDto dto)
    {
        await identityService.SignUpUserAsync(dto);
        return NoContent();
    }

    [Authorize(Policy)]
    [HttpPost("sign-up-admin")]
    public async Task<ActionResult> SignUpAdminAsync(SignUpDto dto)
    {
        await identityService.SignUpAdminAsync(dto);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JsonWebToken>> SignInAsync(SignInDto dto)
        => Ok(await identityService.SignInAsync(dto));
}