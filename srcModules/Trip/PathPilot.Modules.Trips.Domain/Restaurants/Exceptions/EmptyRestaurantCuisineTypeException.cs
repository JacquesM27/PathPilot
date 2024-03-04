using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

public sealed class EmptyRestaurantCuisineTypeException()
    : PathPilotException("Restaurant defines empty cuisine type")
{
    
}