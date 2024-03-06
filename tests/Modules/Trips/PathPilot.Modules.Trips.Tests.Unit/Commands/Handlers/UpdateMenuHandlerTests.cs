using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Kernel.Types;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Commands.Handlers
{
    public class UpdateMenuHandlerTests
    {
        private readonly ICommandHandler<UpdateMenu> _commandHandler;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly Restaurant _restaurant;

        public UpdateMenuHandlerTests()
        {
            _restaurantRepository = Substitute.For<IRestaurantRepository>();
            _commandHandler = new UpdateMenuHandler(_restaurantRepository);
            _restaurant = RestaurantHelper.GetRestaurant();
        }

        private Task Act(UpdateMenu command) => _commandHandler.HandleAsync(command);

        [Fact]
        public async Task HandleUpdateMenu_ShouldUpdateRestaurantMenu()
        {
            // Arrange
            var command = new UpdateMenu(
                "TestRestaurantId",
                new List<MenuItemToUpdate>
                {
                    new ("Pizza", "Delicious pizza", 10.99),
                    new ("Burger", "Tasty burger", 8.99)
                }
            );
            _restaurantRepository.GetAsync(command.RestaurantId).Returns(_restaurant);

            // Act
            await Act(command);

            // Assert
            await _restaurantRepository.Received(1).UpdateAsync(Arg.Is<Restaurant>(r =>
                r.MenuItems.Count() == command.Items.Count() &&
                r.MenuItems.All(menuItem =>
                    command.Items.Any(item =>
                        item.Name == menuItem.Name &&
                        item.Description == menuItem.Description &&
                        Equals(item.Price, menuItem.Price)))));
            await _restaurantRepository.Received(1).GetAsync("TestRestaurantId");
        }

        [Fact]
        public async Task HandleUpdateMenu_ShouldThrowRestaurantNotFoundException_WhenRestaurantNotFound()
        {
            // Arrange
            var command = new UpdateMenu(
                "TestRestaurantId",
                new List<MenuItemToUpdate>
                {
                    new ("Pizza", "Delicious pizza", 10.99),
                    new ("Burger", "Tasty burger", 8.99)
                }
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

        [Fact]
        public async Task HandleUpdateMenu_ShouldThrowNoMenuItemsToAddException_WhenMenuItemsListIsEmpty()
        {
            // Arrange
            var command = new UpdateMenu("TestRestaurantId", new List<MenuItemToUpdate>());

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            await _restaurantRepository.Received(0).GetAsync(Arg.Any<EntityId>());
            await _restaurantRepository.Received(0).UpdateAsync(Arg.Any<Restaurant>());
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoMenuItemsToAddException>();
        }
    }
}
