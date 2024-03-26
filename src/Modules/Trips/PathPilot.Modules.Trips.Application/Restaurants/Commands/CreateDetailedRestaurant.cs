using System.Text.Json.Serialization;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed class CreateDetailedRestaurant(string name, string description, string cuisineType, string city, 
    string street, string buildingNumber, 
    string postCode, string country, IEnumerable<MenuItemRecord> items) : ICommand
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string CuisineType { get; } = cuisineType;
    public string City { get; } = city;
    public string Street { get; } = street;
    public string BuildingNumber { get; } = buildingNumber;
    public string PostCode { get; } = postCode;
    public string Country { get; } = country;
    public IEnumerable<MenuItemRecord> Items { get; } = items;
    [JsonIgnore]
    public Guid Id { get; set; }
    [JsonIgnore]
    public Guid OwnerId { get; set; }
}