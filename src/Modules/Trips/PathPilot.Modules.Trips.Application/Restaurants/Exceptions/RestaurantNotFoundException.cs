using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Application.Restaurants.Exceptions;

public sealed class RestaurantNotFoundException(string id)
    : PathPilotException($"Restaurant with ID: '{id}' was not found.")
{
}