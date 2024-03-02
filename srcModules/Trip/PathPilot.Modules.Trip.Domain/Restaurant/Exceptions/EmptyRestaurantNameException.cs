using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.Restaurant.Exceptions;

public sealed class EmptyRestaurantNameException()
    : PathPilotException("Restaurant defines empty name.");
