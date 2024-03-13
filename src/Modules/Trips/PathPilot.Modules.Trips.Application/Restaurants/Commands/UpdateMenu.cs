using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record UpdateMenu(Guid RestaurantId, IEnumerable<MenuItemToUpdate> Items) : Command;

public sealed record MenuItemToUpdate(string Name, string? Description = null, double? Price = null);