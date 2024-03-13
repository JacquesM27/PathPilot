using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Application.Restaurants.Queries;
using PathPilot.Modules.Trips.Application.Restaurants.Queries.Handlers;
using PathPilot.Modules.Trips.Application.Shared.Mappings;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Shared.Abstractions.Queries;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Queries.Handlers;

public class GetRestaurantHandlerTests
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IQueryHandler<GetRestaurant,RestaurantDetailsDto> _queryHandler;
    private readonly Restaurant _restaurant;

    public GetRestaurantHandlerTests()
    {
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        _queryHandler = new GetRestaurantHandler(_restaurantRepository);
        _restaurant = RestaurantHelper.GetRestaurant();
    }

    private Task<RestaurantDetailsDto> Act(GetRestaurant query) => _queryHandler.HandleAsync(query);
    
    [Fact]
    public async Task HandleGetRestaurant_ShouldThrowRestaurantNotFoundException_WhenRestaurantNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var query = new GetRestaurant(id);
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RestaurantNotFoundException>();
        await _restaurantRepository.Received(1).GetAsync(id);
    }

    [Fact]
    public async Task HandleGetRestaurant_ShouldOpenRestaurant()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new GetRestaurant(id);
        _restaurantRepository.GetAsync(id).Returns(_restaurant);
        
        // Act
        var restaurantDto = await Act(command);

        // Assert        
        _restaurant.IsOpened.ShouldBeTrue();
        await _restaurantRepository.Received(1).GetAsync(id);
        restaurantDto.Name.ShouldBe(_restaurant.Name);
        restaurantDto.Description.ShouldBe(_restaurant.Description);
        restaurantDto.IsOpened.ShouldBe(_restaurant.IsOpened);
        restaurantDto.AverageRate.ShouldBe(_restaurant.AverageRate);
        restaurantDto.CuisineType.ShouldBe(_restaurant.CuisineType);
        restaurantDto.Address.ShouldBe(_restaurant.Address.ToDto());
        restaurantDto.MenuItems.ShouldNotBeNull();
        restaurantDto.MenuItems.Count().ShouldBe(_restaurant.MenuItems.Count());
        for (var i = 0; i < restaurantDto.MenuItems.Count(); i++)
        {
            restaurantDto.MenuItems.ElementAt(i).Name.ShouldBe(_restaurant.MenuItems.ElementAt(i).Name);
            restaurantDto.MenuItems.ElementAt(i).Description.ShouldBe(_restaurant.MenuItems.ElementAt(i).Description);
            restaurantDto.MenuItems.ElementAt(i).Price.ShouldBe(_restaurant.MenuItems.ElementAt(i).Price);
        }
    }
}