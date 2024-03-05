using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class UpdateAddressHandler(
    IRestaurantRepository restaurantRepository
    ) : ICommandHandler<UpdateAddress>
{
    public async Task HandleAsync(UpdateAddress command)
    {
        var restaurant = await restaurantRepository.GetAsync(command.RestaurantId)
                         ?? throw new RestaurantNotFoundException(command.RestaurantId);
        
        var address = new Address(command.City, command.Street, command.BuildingNumber, command.PostCode,
            command.Country);
        
        restaurant.UpdateAddress(address);
        await restaurantRepository.UpdateAsync(restaurant);
        // TODO: dispatch address event
    }
}