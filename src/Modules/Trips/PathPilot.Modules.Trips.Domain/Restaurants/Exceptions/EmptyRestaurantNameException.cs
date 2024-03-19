using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

public sealed class EmptyRestaurantNameException()
    : PathPilotException("Restaurant defines empty name.");
