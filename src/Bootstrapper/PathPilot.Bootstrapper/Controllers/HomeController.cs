using Microsoft.AspNetCore.Mvc;

namespace PathPilot.Bootstrapper.Controllers;

[Route("")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("PathPilot API");
    }
}