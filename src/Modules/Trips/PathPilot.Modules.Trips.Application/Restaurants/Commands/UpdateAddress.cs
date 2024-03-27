using System.Text.Json.Serialization;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed class UpdateAddress(
    Guid restaurantId,
    string city,
    string street,
    string buildingNumber,
    string postCode,
    string country) : ICommand
{
    public Guid RestaurantId { get; } = restaurantId;
    public string City { get; } = city;
    public string Street { get; } = street;
    public string BuildingNumber { get; } = buildingNumber;
    public string PostCode { get; } = postCode;
    public string Country { get; } = country;
    
    [JsonIgnore]
    public Guid UserId { get; set; }
}
