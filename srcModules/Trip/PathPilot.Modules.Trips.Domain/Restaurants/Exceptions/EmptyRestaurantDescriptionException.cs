using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

public sealed class EmptyRestaurantDescriptionException()
    : PathPilotException("Restaurant defines empty description");
