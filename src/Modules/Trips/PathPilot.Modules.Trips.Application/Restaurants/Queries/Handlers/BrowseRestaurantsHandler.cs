using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Kernel.Types;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Modules.Trips.Application.Restaurants.Queries.Handlers;

internal sealed class BrowseRestaurantsHandler(
    IRestaurantRepository restaurantRepository
    ) : IQueryHandler<BrowseRestaurants, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> HandleAsync(BrowseRestaurants query)
    {
        IEnumerable<Restaurant> restaurants;

        if (query.ids is null || !query.ids.Any())
            restaurants = await restaurantRepository.BrowseAsync();
        else
            restaurants = await restaurantRepository.BrowseAsync(query.ids.Select(id => new EntityId(id)));

        return restaurants.Select(r => r.ToDto());
    }
}