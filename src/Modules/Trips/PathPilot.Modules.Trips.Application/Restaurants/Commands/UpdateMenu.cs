using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record UpdateMenu(string RestaurantId, IEnumerable<MenuItemToUpdate> Items) : Command;

public sealed record MenuItemToUpdate(string Name, string? Description = null, double? Price = null);