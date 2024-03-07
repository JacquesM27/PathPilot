using PathPilot.Shared.Abstractions.Events;

namespace PathPilot.Modules.Trips.Application.Restaurants.Events;

public record RestaurantAddressUpdated(string RestaurantId) : IEvent;