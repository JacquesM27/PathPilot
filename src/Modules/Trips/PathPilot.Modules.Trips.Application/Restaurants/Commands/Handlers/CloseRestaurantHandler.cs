using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class CloseRestaurantHandler(
    IRestaurantRepository restaurantRepository
    ) : ICommandHandler<CloseRestaurant>
{
    public async Task HandleAsync(CloseRestaurant command)
    {
        var restaurant = await restaurantRepository.GetAsync(command.Id)
            ?? throw new RestaurantNotFoundException(command.Id);
        
        restaurant.Close();
        await restaurantRepository.UpdateAsync(restaurant);
    }
}