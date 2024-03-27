using System.Text.Json.Serialization;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed class CreateRestaurant(string name, string description, string cuisineType) : ICommand
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string CuisineType { get; } = cuisineType;
    [JsonIgnore]
    public Guid Id { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
}