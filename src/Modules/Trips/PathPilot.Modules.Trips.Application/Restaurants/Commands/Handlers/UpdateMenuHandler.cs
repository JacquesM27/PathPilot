using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Application.Restaurants.Policies;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class UpdateMenuHandler(
    IRestaurantRepository restaurantRepository,
    IRestaurantManagementPolicy restaurantManagementPolicy
    ) : ICommandHandler<UpdateMenu>
{
    public async Task HandleAsync(UpdateMenu command)
    {
        var restaurant = await restaurantRepository.GetAsync(command.RestaurantId)
            ?? throw new RestaurantNotFoundException(command.RestaurantId);
        
        
        if (!restaurantManagementPolicy.CanManageRestaurant(command.UserId, restaurant))
            throw new CannotManageRestaurantException(command.UserId);
        
        var duplications = command.Items
            .GroupBy(item => item.Name)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key).ToList();

        if (duplications.Count != 0)
            throw new DuplicatedMenuItemToAddException(command.RestaurantId, duplications);
        
        var itemsToUpdate = command.Items ?? [];
        
        
        restaurant.UpdateMenu(itemsToUpdate.MapToMenuItems());
        await restaurantRepository.UpdateAsync(restaurant);
    }
}