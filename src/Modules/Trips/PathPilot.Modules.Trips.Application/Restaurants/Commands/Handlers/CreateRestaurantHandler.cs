using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class CreateRestaurantHandler(
    IRestaurantRepository restaurantRepository
    ) : ICommandHandler<CreateRestaurant>
{
    public async Task HandleAsync(CreateRestaurant command)
    {
        var restaurant = Restaurant.Create(command.Name, command.Description, command.CuisineType, command.OwnerId);

        await restaurantRepository.AddAsync(restaurant);
        command.Id = restaurant.Id;
    }
}