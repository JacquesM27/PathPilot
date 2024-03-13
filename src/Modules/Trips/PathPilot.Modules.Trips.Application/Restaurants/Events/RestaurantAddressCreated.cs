using PathPilot.Shared.Abstractions.Events;

namespace PathPilot.Modules.Trips.Application.Restaurants.Events;

public record RestaurantAddressCreated(Guid RestaurantId, string City, string Street, string BuildingNumber, 
    string PostCode, string Country) : IEvent;