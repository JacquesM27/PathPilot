using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

public sealed class EmptyMenuItemNameException()
    : PathPilotException("Menu item defines empty name")
{
    
}