using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Modules.Trips.Application.Restaurants.Queries.Handlers;

internal sealed class GetRestaurantHandler(
    IRestaurantRepository restaurantRepository
    ) : IQueryHandler<GetRestaurant, RestaurantDto>
{
    public async Task<RestaurantDto> HandleAsync(GetRestaurant query)
    {
        var restaurant = await restaurantRepository.GetAsync(query.Id)
                         ?? throw new RestaurantNotFoundException(query.Id);

        return restaurant.ToDto();
    }
}