using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Events;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Messaging;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Commands.Handlers
{
    public class UpdateAddressHandlerTests
    {
        private readonly ICommandHandler<UpdateAddress> _commandHandler;
        private readonly IMessageBroker _messageBroker;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly Restaurant _restaurant;

        public UpdateAddressHandlerTests()
        {
            _restaurantRepository = Substitute.For<IRestaurantRepository>();
            _messageBroker = Substitute.For<IMessageBroker>();
            _commandHandler = new UpdateAddressHandler(_restaurantRepository, _messageBroker);
            _restaurant = RestaurantHelper.GetRestaurant();
        }
        
        private Task Act(UpdateAddress command) => _commandHandler.HandleAsync(command); 

        [Fact]
        public async Task HandleAsync_ShouldUpdateRestaurantAddress()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateAddress(
                id,
                "New York",
                "Broadway",
                "123",
                "10001",
                "USA"
            );
            _restaurantRepository.GetAsync(command.RestaurantId).Returns(_restaurant);

            // Act
            await Act(command);

            // Assert
            await _restaurantRepository.Received(1).UpdateAsync(Arg.Is<Restaurant>(r =>
                r.Address.City == command.City &&
                r.Address.Street == command.Street &&
                r.Address.BuildingNumber == command.BuildingNumber &&
                r.Address.PostCode == command.PostCode &&
                r.Address.Country == command.Country));
            
            await _restaurantRepository.Received(1).GetAsync(id);
            await _messageBroker.Received(1).PublishAsync(Arg.Any<RestaurantAddressCreated>());
            await _messageBroker.Received(1).PublishAsync(Arg.Is<RestaurantAddressCreated>(message =>
                message.RestaurantId == _restaurant.Id &&
                message.City == command.City &&
                message.Street == command.Street &&
                message.BuildingNumber == command.BuildingNumber &&
                message.PostCode == command.PostCode &&
                message.Country == command.Country
            ));
        }

        [Fact]
        public async Task HandleAsync_ShouldThrowRestaurantNotFoundException_WhenRestaurantNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateAddress(
                id,
                "New York",
                "Broadway",
                "123",
                "10001",
                "USA"
            );
            _restaurantRepository.GetAsync(command.RestaurantId)!.Returns((Restaurant)null!);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            await _restaurantRepository.Received(1).GetAsync(id);
            await _restaurantRepository.Received(0).UpdateAsync(default!);
            await _messageBroker.Received(0).PublishAsync(default!);
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<RestaurantNotFoundException>();
        }
    }
}
