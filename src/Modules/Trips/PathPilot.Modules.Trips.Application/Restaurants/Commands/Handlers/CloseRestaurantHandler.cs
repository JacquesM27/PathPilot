using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Policies;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class CloseRestaurantHandler(
    IRestaurantRepository restaurantRepository,
    IRestaurantManagementPolicy restaurantManagementPolicy
    ) : ICommandHandler<CloseRestaurant>
{
    public async Task HandleAsync(CloseRestaurant command)
    {
        var restaurant = await restaurantRepository.GetAsync(command.Id)
            ?? throw new RestaurantNotFoundException(command.Id);

        if (!restaurantManagementPolicy.CanManageRestaurant(command.UserId, restaurant))
            throw new CannotManageRestaurantException(command.UserId);
        
        restaurant.Close();
        await restaurantRepository.UpdateAsync(restaurant);
    }
}