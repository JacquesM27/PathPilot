using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record CreateRestaurant(string Name, string Description, string CuisineType) : Command
{
    public Guid Id { get; set; }
}
