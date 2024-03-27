using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record OpenRestaurant(Guid Id, Guid UserId) : ICommand;