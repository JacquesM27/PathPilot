using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Domain.Tests.Helpers;
using PathPilot.Modules.Trips.Domain.ValueObjects;

namespace PathPilot.Modules.Trips.Domain.Tests.Domain.Entities;

public class RestaurantTests
{
    private readonly Restaurant _restaurant;

    // Setup
    public RestaurantTests()
    {
        _restaurant = RestaurantHelper.GetRestaurant();
    }
    
    [Fact]
    public void GivenParametersWithInvalidName_ShouldFail()
    {
        // Arrange
        const string id = "1234";
        var name = string.Empty;
        const string description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;

        // Act
        var exception = Record.Exception(() => Restaurant.Create(name, description, cuisine));
        
        // Arrange
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyRestaurantNameException>();
    }
    
    [Fact]
    public void GivenParametersWithValidParameters_ShouldPass()
    {
        // Arrange
        const string name = "Pasta Italiano";
        const string description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;

        // Act
        var restaurant = Restaurant.Create(name, description, cuisine);
        
        // Arrange
        restaurant.Name.Value.ShouldBe(name);
        restaurant.Description.Value.ShouldBe(description);
        restaurant.CuisineType.Value.ShouldBe(cuisine);
        restaurant.MenuItems.ShouldNotBeNull();
        restaurant.MenuItems.ShouldBeEmpty();
        restaurant.Address.ShouldBeNull();
    }

    [Fact]
    public void GivenMenuItemWithInvalidName_ShouldFail()
    {
        // Arrange
        var name = string.Empty;
        
        // Act
        var exception = Record.Exception(() => new MenuItem(name));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyMenuItemNameException>();
    }

    [Fact]
    public void GivenMenuItemWithValidName_ShouldPass()
    {
        // Arrange
        const string name = "Lasagne";
        
        // Act
        var menuitem = new MenuItem(name);
        
        // Assert
        menuitem.Name.ShouldBe(name);
        menuitem.Description.ShouldBeNull();
        menuitem.Price.ShouldBeNull();
    }

    [Fact]
    public void GivenMenuItemWithOptionalParameters_ShouldHaveValues()
    {
        // Arrange
        const string name = "Lasagne";
        const string description = "Lasagna is an Italian dish consisting of layers of flat pasta, tomato sauce, meat, béchamel sauce, and mozzarella cheese, creating a savory and flavorful baked dish.";
        const double price = 21.37;
        
        // Act
        var menuitem = new MenuItem(name, description, price);
        
        // Assert
        menuitem.Name.ShouldBe(name);
        menuitem.Description.ShouldBe(description);
        menuitem.Price.ShouldBe(price);
    }

    [Fact]
    public void GivenRestaurant_ShouldAddMenuItemsToRestaurantCollection()
    {
        // Arrange
        MenuItem[] menuItems =
        [
            new MenuItem("Carbonara"),
            new MenuItem("Lasagne")
        ];
        
        // Act
        _restaurant.UpdateMenu(menuItems);
        
        // Assert
        _restaurant.MenuItems.ShouldNotBeEmpty();
        _restaurant.MenuItems.Count().ShouldBe(menuItems.Length);
        _restaurant.MenuItems.ShouldContain(menuItems.First());
    }

    [Fact]
    public void GivenRestaurant_ShouldUpdateAddressProperly()
    {
        // Arrange
        var address = new Address("Warsaw", "Złota", "1", "00-000", "Poland");
        
        // Act
        _restaurant.UpdateAddress(address);
        
        // Assert
        _restaurant.Address.ShouldNotBeNull();
        _restaurant.Address.ShouldBe(address);
    }
}