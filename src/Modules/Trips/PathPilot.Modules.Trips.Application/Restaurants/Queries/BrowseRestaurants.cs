using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Modules.Trips.Application.Restaurants.Queries;

public sealed record BrowseRestaurants(IEnumerable<string>? ids = null) : IQuery<IEnumerable<RestaurantDto>>;