using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
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
        if (command.Items is null || !command.Items.Any())
            throw new NoMenuItemsToAddException(command.RestaurantId);
        
        var restaurant = await restaurantRepository.GetAsync(command.RestaurantId)
            ?? throw new RestaurantNotFoundException(command.RestaurantId);
        
        restaurant.UpdateMenu(MapToMenuItems(command.Items));
        await restaurantRepository.UpdateAsync(restaurant);
    }
    
    private static IEnumerable<MenuItem> MapToMenuItems(IEnumerable<MenuItemToUpdate> itemsToAdd)
        => itemsToAdd.Select(item => new MenuItem(item.Name, item.Description, item.Price));
}