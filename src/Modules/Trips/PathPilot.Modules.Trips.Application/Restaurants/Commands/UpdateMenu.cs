using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record UpdateMenu(Guid RestaurantId, IEnumerable<MenuItemRecord> Items) : ICommand;