using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record CreateDetailedRestaurant(string Name, string Description, string CuisineType, string City, string Street, string BuildingNumber, 
    string PostCode, string Country, IEnumerable<MenuItemRecord> Items) : ICommand
{
    public Guid Id { get; set; }
}