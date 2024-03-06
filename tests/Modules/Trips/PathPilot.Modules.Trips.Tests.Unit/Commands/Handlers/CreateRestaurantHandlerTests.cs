using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Domain.Tests.Commands.Handlers
{
    public class CreateRestaurantHandlerTests
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ICommandHandler<CreateRestaurant> _commandHandler;

        public CreateRestaurantHandlerTests()
        {
            _restaurantRepository = Substitute.For<IRestaurantRepository>();
            _commandHandler = new CreateRestaurantHandler(_restaurantRepository);
        }

        [Fact]
        public async Task given_restaurant_should_create_restaurant()
        {
            // Arrange
            var command = new CreateRestaurant("Pasta Italiano", "Description of the restaurant", CuisineType.Italian);

            // Act
            await _commandHandler.HandleAsync(command);

            // Assert
            await _restaurantRepository.Received(1).AddAsync(Arg.Is<Restaurant>(r =>
                r.Name == command.Name &&
                r.Description == command.Description &&
                r.CuisineType == command.CuisineType));
        }
    }
}