using Microsoft.AspNetCore.Mvc;

namespace PathPilot.Modules.Trips.Api.Controllers;

[Route(TripsModule.BasePath)]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => "Trips API";
}