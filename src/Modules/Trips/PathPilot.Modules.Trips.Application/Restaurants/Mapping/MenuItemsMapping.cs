using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

namespace PathPilot.Modules.Trips.Application.Restaurants.Mapping;

internal static class MenuItemsMapping
{
    internal static IEnumerable<MenuItem> MapToMenuItems(this IEnumerable<MenuItemRecord> itemsToAdd)
        => itemsToAdd.Select(item => new MenuItem(item.Name, item.Description, item.Price));
}