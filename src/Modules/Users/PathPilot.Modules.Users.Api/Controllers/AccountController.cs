using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathPilot.Modules.Users.Core.Services;

namespace PathPilot.Modules.Users.Api.Controllers;

internal sealed class AccountController(
    IIdentityService identityService
    ) : BaseController
{
    
}