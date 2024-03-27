using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Policies;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class OpenRestaurantHandler(
    IRestaurantRepository restaurantRepository,
    IRestaurantManagementPolicy restaurantManagementPolicy
    ) : ICommandHandler<OpenRestaurant>
{
    public async Task HandleAsync(OpenRestaurant command)
    {
        var restaurant = await restaurantRepository.GetAsync(command.Id)
                         ?? throw new RestaurantNotFoundException(command.Id);
        
        if (!restaurantManagementPolicy.CanManageRestaurant(command.UserId, restaurant))
            throw new CannotManageRestaurantException(command.UserId);
        
        restaurant.Open();
        await restaurantRepository.UpdateAsync(restaurant);
    }
}