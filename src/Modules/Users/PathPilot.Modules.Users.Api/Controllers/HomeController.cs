using Microsoft.AspNetCore.Mvc;

namespace PathPilot.Modules.Users.Api.Controllers;

[Route(UsersModule.BasePath)]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => "Trips API";
}