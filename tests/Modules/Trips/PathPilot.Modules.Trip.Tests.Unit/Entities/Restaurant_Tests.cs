using PathPilot.Modules.Trip.Domain.Restaurants.Entities;
using PathPilot.Modules.Trip.Domain.Restaurants.Exceptions;
using PathPilot.Modules.Trip.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trip.Domain.ValueObjects;
using Shouldly;

namespace PathPilot.Modules.Trip.Domain.Tests.Entities;

public class Restaurant_Tests
{
    [Fact]
    public void given_parameters_with_invalid_name_should_fail()
    {
        // Arrange
        var id = "1234";
        var name = string.Empty;
        var description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;
        IEnumerable<MenuItem> menuItems = [];
        var address = new Address("Warsaw", "Złota", "1", "00-000", "Poland", 0, 0);

        // Act
        var exception = Record.Exception(() => Restaurant.Create(id, name, description, cuisine, address, null));
        
        // Arrange
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyRestaurantNameException>();
    }
    
    [Fact]
    public void given_parameters_with_valid_parameters_should_fail()
    {
        // Arrange
        var id = "1234";
        var name = "Pasta Italiano";
        var description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;
        var address = new Address("Warsaw", "Złota", "1", "00-000", "Poland", 0, 0);

        // Act
        var restaurant = Restaurant.Create(id, name, description, cuisine, address, null);
        
        // Arrange
        restaurant.Id.Value.ShouldBe(id);
        restaurant.Name.Value.ShouldBe(name);
        restaurant.Description.Value.ShouldBe(description);
        restaurant.CuisineType.Value.ShouldBe(cuisine);
        restaurant.MenuItems.ShouldNotBeNull();
        restaurant.MenuItems.ShouldBeEmpty();
        restaurant.Address.ShouldBe(address);
    }
}