using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record CloseRestaurant(Guid Id) : Command;