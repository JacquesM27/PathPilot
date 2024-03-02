using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.Restaurant.Exceptions;

public sealed class EmptyRestaurantDescriptionException()
    : PathPilotException("Restaurant defines empty description");
