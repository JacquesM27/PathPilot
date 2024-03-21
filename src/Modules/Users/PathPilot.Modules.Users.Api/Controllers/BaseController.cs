using Microsoft.AspNetCore.Mvc;
using PathPilot.Shared.Infrastructure.Api;

namespace PathPilot.Modules.Users.Api.Controllers;

[Route(UsersModule.BasePath+"/[controller]")]
[ApiController]
[ProducesDefaultContentType]
internal abstract class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
            return NotFound();
        return Ok(model);
    }
}