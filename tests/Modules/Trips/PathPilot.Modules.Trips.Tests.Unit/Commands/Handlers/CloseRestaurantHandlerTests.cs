using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Commands;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Commands.Handlers;

public class CloseRestaurantHandlerTests
{
    private Task Act(CloseRestaurant command) => _commandHandler.HandleAsync(command);
    
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ICommandHandler<CloseRestaurant> _commandHandler;

    [Fact]
    public async Task given_missing_restaurant_should_fail()
    {
        // Arrange
        const string id = "this is the id";
        var command = new CloseRestaurant(id);
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RestaurantNotFoundException>();
        await _restaurantRepository.Received(1).GetAsync(id);
        await _restaurantRepository.Received(0).UpdateAsync(default);
    }

    [Fact]
    public async Task given_restaurant_should_be_closed()
    {
        // Arrange
        const string id = "this is the id";
        var command = new CloseRestaurant(id);
        _restaurantRepository.GetAsync(id).Returns(_restaurant);
        
        // Act
        await Act(command);

        // Assert        
        _restaurant.IsOpened.ShouldBeFalse();
        await _restaurantRepository.Received(1).GetAsync(id);
        await _restaurantRepository.Received(1).UpdateAsync(_restaurant);
        
    }
    
    public CloseRestaurantHandlerTests()
    {
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        _commandHandler = new CloseRestaurantHandler(_restaurantRepository);
        _restaurant = RestaurantHelper.GetRestaurant();
    }

    private readonly Restaurant _restaurant;
}