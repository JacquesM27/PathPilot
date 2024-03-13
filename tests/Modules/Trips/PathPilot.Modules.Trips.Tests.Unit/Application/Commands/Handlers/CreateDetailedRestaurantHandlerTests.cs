using NSubstitute;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Handlers;
using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Modules.Trips.Application.Restaurants.Events;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Messaging;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Commands.Handlers;

public class CreateDetailedRestaurantHandlerTests
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ICommandHandler<CreateDetailedRestaurant> _commandHandler;

    public CreateDetailedRestaurantHandlerTests()
    {
        _restaurantRepository = Substitute.For<IRestaurantRepository>();
        _messageBroker = Substitute.For<IMessageBroker>();
        _commandHandler = new CreateDetailedRestaurantHandler(_restaurantRepository, _messageBroker);
        
    }
    
    [Fact]
    public async Task HandleCreateDetailedRestaurant_ShouldCreateRestaurant()
    {
        // Arrange
        var menuItems = new List<MenuItemRecord>
        {
            new ("Pizza", "Delicious pizza", 10.99),
            new ("Burger", "Tasty burger", 8.99)
        };
        var command = new CreateDetailedRestaurant(
            "Pasta Italiano", 
            "Description of the restaurant", 
            CuisineType.Italian, 
            "City", "Street", "BuildingNumber", "PostCode", "Country",
            menuItems
        );

        // Act
        await _commandHandler.HandleAsync(command);

        // Assert
        await _restaurantRepository.Received(1).AddAsync(Arg.Is<Restaurant>(r =>
                r.Name == command.Name &&
                r.Description == command.Description &&
                r.CuisineType == command.CuisineType &&
                r.Address != null &&
                r.Address.City == command.City &&
                r.Address.Street == command.Street &&
                r.Address.BuildingNumber == command.BuildingNumber &&
                r.Address.PostCode == command.PostCode &&
                r.Address.Country == command.Country &&
                r.MenuItems.Count() == menuItems.Count
        ));
        command.Id.ShouldNotBe(Guid.Empty);
        await _messageBroker.Received(1).PublishAsync(Arg.Is<RestaurantAddressCreated>(message =>
            message.RestaurantId == command.Id &&
            message.City == command.City &&
            message.Street == command.Street &&
            message.BuildingNumber == command.BuildingNumber &&
            message.PostCode == command.PostCode &&
            message.Country == command.Country
        ));
    }
}