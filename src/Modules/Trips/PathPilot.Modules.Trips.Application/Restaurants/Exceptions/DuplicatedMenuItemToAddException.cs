using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Application.Restaurants.Exceptions;

public sealed class DuplicatedMenuItemToAddException(string restaurantId, IEnumerable<string> names)
    : PathPilotException($"Duplicated menu item(s) with name(s): '{string.Join(", ",names)}' to add for restaurant with id: '{restaurantId}'.")
{
    public string RestaurantId => restaurantId;
}