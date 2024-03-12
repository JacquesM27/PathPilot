using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Storage;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class CreateRestaurantHandler(
    IRestaurantRepository restaurantRepository,
    IRequestStorage requestStorage
    ) : ICommandHandler<CreateRestaurant>
{
    public async Task HandleAsync(CreateRestaurant command)
    {
        var restaurant = Restaurant.Create(command.Name, command.Description, command.CuisineType);

        await restaurantRepository.AddAsync(restaurant);
        requestStorage.Set(command.CommandId, restaurant.Id.Value);
    }
}