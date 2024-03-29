﻿using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Policies;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Commands.Handlers;

public class OpenRestaurantHandlerTests
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ICommandHandler<OpenRestaurant> _commandHandler;
    private readonly Restaurant _restaurant;

    public OpenRestaurantHandlerTests()
    {
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        var restaurantManagementPolicy = new RestaurantManagementPolicy();
        _commandHandler = new OpenRestaurantHandler(_restaurantRepository, restaurantManagementPolicy);
        _restaurant = RestaurantHelper.GetRestaurant();
    }
    
    private Task Act(OpenRestaurant command) => _commandHandler.HandleAsync(command);
    
    [Fact]
    public async Task HandleOpenRestaurant_ShouldThrowRestaurantNotFoundException_WhenRestaurantNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new OpenRestaurant(id, Guid.NewGuid());
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RestaurantNotFoundException>();
        await _restaurantRepository.Received(1).GetAsync(id);
        await _restaurantRepository.Received(0).UpdateAsync(default);
    }

    
    [Fact]
    public async Task HandleOpenRestaurant_ShouldThrowCannotManageRestaurantException_WhenUserIsNotOwner()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new OpenRestaurant(id, Guid.NewGuid());
        _restaurantRepository.GetAsync(id).Returns(_restaurant);
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotManageRestaurantException>();
        await _restaurantRepository.Received(1).GetAsync(id);
        await _restaurantRepository.Received(0).UpdateAsync(default);
        
    }
    
    [Fact]
    public async Task HandleOpenRestaurant_ShouldOpenRestaurant()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new OpenRestaurant(id, RestaurantHelper.OwnerId);
        _restaurantRepository.GetAsync(id).Returns(_restaurant);
        
        // Act
        await Act(command);

        // Assert        
        _restaurant.IsOpened.ShouldBeTrue();
        await _restaurantRepository.Received(1).GetAsync(id);
        await _restaurantRepository.Received(1).UpdateAsync(_restaurant);
        
    }
}