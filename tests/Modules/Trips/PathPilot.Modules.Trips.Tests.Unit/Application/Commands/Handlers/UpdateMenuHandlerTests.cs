using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Commands;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Commands.Handlers
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
            var id = Guid.NewGuid();
            var command = new UpdateMenu(
                id,
                new List<MenuItemRecord>
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
            await _restaurantRepository.Received(1).GetAsync(id);
        }

        [Fact]
        public async Task HandleUpdateMenu_ShouldThrowRestaurantNotFoundException_WhenRestaurantNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateMenu(
                id,
                new List<MenuItemRecord>
                {
                    new ("Pizza", "Delicious pizza", 10.99),
                    new ("Burger", "Tasty burger", 8.99)
                }
            );
            _restaurantRepository.GetAsync(command.RestaurantId).Returns((Restaurant)null!);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            await _restaurantRepository.Received(1).GetAsync(id);
            await _restaurantRepository.Received(0).UpdateAsync(default!);
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<RestaurantNotFoundException>();
        }
        
        [Fact]
        public async Task HandleUpdateMenu_ShouldUpdateMenu()
        {
            // Arrange
            var id = Guid.NewGuid();
            var menuItem1 = new Restaurants.ValueObjects.MenuItem("Pizza", "Delicious pizza", 10.99);
            var menuItem2 = new Restaurants.ValueObjects.MenuItem("Burger", "Tasty burger", 8.99);
            _restaurant.UpdateMenu(
            [
                menuItem1,
                menuItem2
            ]);
            
            var command = new UpdateMenu(
                id,
                new List<MenuItemRecord>
                {
                    new ("Pizza", "Updated pizza description", 11.99),
                    new ("Pasta", "Delicious pasta", 12.99),
                }
            );


            _restaurantRepository.GetAsync(command.RestaurantId).Returns(_restaurant);

            // Act
            await Act(command);

            // Assert
            await _restaurantRepository.Received(1).UpdateAsync(Arg.Is<Restaurant>(r =>
                r.MenuItems.Count() == 2 &&
                r.MenuItems.Any(item =>
                    item.Name == "Pizza" &&
                    item.Description == "Updated pizza description" &&
                    item.Price.Equals(11.99)) &&
                r.MenuItems.Any(item =>
                    item.Name == "Pasta" &&
                    item.Description == "Delicious pasta" &&
                    item.Price.Equals(12.99))));
            
            _restaurant.MenuItems.Count().ShouldBe(2);
            _restaurant.MenuItems.ShouldNotContain(item => item.Name == "Burger");
            await _restaurantRepository.Received(1).GetAsync(id);
        }

        [Fact]
        public async Task HandleUpdateMenu_ShouldClearMenu_WhenPassedEmptyList()
        {
            // Arrange
            var id = Guid.NewGuid();
            var menuItem1 = new Restaurants.ValueObjects.MenuItem("Pizza", "Delicious pizza", 10.99);
            var menuItem2 = new Restaurants.ValueObjects.MenuItem("Burger", "Tasty burger", 8.99);
            _restaurant.UpdateMenu(
            [
                menuItem1,
                menuItem2
            ]);
        
            var command = new UpdateMenu(
                id,
                []
            );
        
            _restaurantRepository.GetAsync(command.RestaurantId).Returns(_restaurant);
        
            // Act
            await Act(command);
        
            // Assert
            await _restaurantRepository.Received(1).UpdateAsync(Arg.Is<Restaurant>(r =>
                !r.MenuItems.Any()));
            await _restaurantRepository.Received(1).GetAsync(id);
            _restaurant.MenuItems.ShouldBeEmpty();
        }
    }
}
