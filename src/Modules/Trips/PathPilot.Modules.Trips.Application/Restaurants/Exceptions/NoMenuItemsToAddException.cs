using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Application.Restaurants.Exceptions;

public sealed class NoMenuItemsToAddException(string restaurantId)
    : PathPilotException($"Missing menu items to add for restaurant with id: '{restaurantId}'.")
{
    public string RestaurantId => restaurantId;
}