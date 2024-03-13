using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

public sealed class UpdateMenuHandler(
    IRestaurantRepository restaurantRepository
    ) : ICommandHandler<UpdateMenu>
{
    public async Task HandleAsync(UpdateMenu command)
    {
        var duplications = command.Items
            .GroupBy(item => item.Name)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key).ToList();

        if (duplications.Count != 0)
            throw new DuplicatedMenuItemToAddException(command.RestaurantId, duplications);
        
        var itemsToUpdate = command.Items ?? [];
        
        var restaurant = await restaurantRepository.GetAsync(command.RestaurantId)
            ?? throw new RestaurantNotFoundException(command.RestaurantId);
        
        restaurant.UpdateMenu(itemsToUpdate.MapToMenuItems());
        await restaurantRepository.UpdateAsync(restaurant);
    }
}