using System.Text.Json.Serialization;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed class UpdateMenu(Guid restaurantId, IEnumerable<MenuItemRecord> items) : ICommand
{
    public Guid RestaurantId { get; } = restaurantId;
    public IEnumerable<MenuItemRecord> Items { get; } = items;
    
    [JsonIgnore]
    public Guid UserId { get; set; }
}