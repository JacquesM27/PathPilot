using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class CreateDetailedRestaurantHandler(
    IRestaurantRepository restaurantRepository
) : ICommandHandler<CreateDetailedRestaurant>
{
    public async Task HandleAsync(CreateDetailedRestaurant command)
    {
        
        var address = new Address(command.City, command.Street, command.BuildingNumber, command.PostCode,
            command.Country);
        
        var items = command.Items ?? [];
        
        var restaurant = Restaurant.CreateDetailed(command.Name, command.Description, command.CuisineType, address, 
            items.MapToMenuItems());

        await restaurantRepository.AddAsync(restaurant);
        command.Id = restaurant.Id;
    }
}