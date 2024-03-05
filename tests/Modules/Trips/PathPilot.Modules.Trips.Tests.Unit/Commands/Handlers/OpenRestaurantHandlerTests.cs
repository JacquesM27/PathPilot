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

public class OpenRestaurantHandlerTests
{
    private Task Act(OpenRestaurant command) => _commandHandler.HandleAsync(command);
    
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ICommandHandler<OpenRestaurant> _commandHandler;

    [Fact]
    public async Task given_missing_restaurant_should_fail()
    {
        // Arrange
        const string id = "this is the id";
        var command = new OpenRestaurant(id);
        
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
        var command = new OpenRestaurant(id);
        _restaurantRepository.GetAsync(id).Returns(_restaurant);
        
        // Act
        await Act(command);

        // Assert        
        _restaurant.IsOpened.ShouldBeTrue();
        await _restaurantRepository.Received(1).GetAsync(id);
        await _restaurantRepository.Received(1).UpdateAsync(_restaurant);
        
    }
    
    public OpenRestaurantHandlerTests()
    {
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        _commandHandler = new OpenRestaurantHandler(_restaurantRepository);
        _restaurant = RestaurantHelper.GetRestaurant();
    }

    private Restaurant _restaurant;
}