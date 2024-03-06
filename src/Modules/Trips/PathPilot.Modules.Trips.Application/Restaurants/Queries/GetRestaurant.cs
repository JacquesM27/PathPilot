using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Modules.Trips.Application.Restaurants.Queries;

public sealed record GetRestaurant() : IQuery<IEnumerable<RestaurantDto>>;