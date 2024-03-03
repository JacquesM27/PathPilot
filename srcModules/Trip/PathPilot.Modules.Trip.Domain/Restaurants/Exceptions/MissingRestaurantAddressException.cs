using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.Restaurants.Exceptions;

public sealed class MissingRestaurantAddressException()
    : PathPilotException("Restaurant defines empty address")
{
    
}