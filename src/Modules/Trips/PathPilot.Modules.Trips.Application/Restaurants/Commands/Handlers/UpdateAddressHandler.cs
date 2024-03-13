using PathPilot.Modules.Trips.Application.Restaurants.Events;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Messaging;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;

internal sealed class UpdateAddressHandler(
    IRestaurantRepository restaurantRepository,
    IMessageBroker messageBroker
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
        
        await messageBroker.PublishAsync(new RestaurantAddressCreated(restaurant.Id,
            restaurant.Address.City,
            restaurant.Address.Street,
            restaurant.Address.BuildingNumber,
            restaurant.Address.PostCode,
            restaurant.Address.Country));
    }
}