using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Commands;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Commands.Handlers
{
    public class UpdateAddressHandlerTests
    {
        private readonly ICommandHandler<UpdateAddress> _commandHandler;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly Restaurant _restaurant;

        public UpdateAddressHandlerTests()
        {
            _restaurantRepository = Substitute.For<IRestaurantRepository>();
            _commandHandler = new UpdateAddressHandler(_restaurantRepository);
            _restaurant = RestaurantHelper.GetRestaurant();
        }
        
        private Task Act(UpdateAddress command) => _commandHandler.HandleAsync(command); 

        [Fact]
        public async Task HandleAsync_ShouldUpdateRestaurantAddress()
        {
            // Arrange
            var command = new UpdateAddress(
                "TestRestaurantId",
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
            await _restaurantRepository.Received(1).GetAsync("TestRestaurantId");
        }

        [Fact]
        public async Task HandleAsync_ShouldThrowRestaurantNotFoundException_WhenRestaurantNotFound()
        {
            // Arrange
            var command = new UpdateAddress(
                "TestRestaurantId",
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
            await _restaurantRepository.Received(1).GetAsync("TestRestaurantId");
            await _restaurantRepository.Received(0).UpdateAsync(default!);
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<RestaurantNotFoundException>();
        }
    }
}
