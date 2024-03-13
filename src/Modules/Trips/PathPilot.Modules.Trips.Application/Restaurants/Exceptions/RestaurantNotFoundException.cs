using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Application.Restaurants.Exceptions;

public sealed class RestaurantNotFoundException(Guid id)
    : PathPilotException($"Restaurant with ID: '{id:D}' was not found.")
{
}