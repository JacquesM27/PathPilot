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
    public void given_parameters_with_valid_parameters_should_pass()
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

    [Fact]
    public void given_menu_item_with_invalid_name_should_fail()
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
    public void given_menu_item_with_valid_name_should_pass()
    {
        // Arrange
        var name = "Lasagne";
        
        // Act
        var menuitem = new MenuItem(name);
        
        // Assert
        menuitem.Name.ShouldBe(name);
        menuitem.Description.ShouldBeNull();
        menuitem.Price.ShouldBeNull();
    }

    [Fact]
    public void given_menu_item_with_optional_parameters_should_has_values()
    {
        // Arrange
        var name = "Lasagne";
        var description = "Lasagna is an Italian dish consisting of layers of flat pasta, tomato sauce, meat, béchamel sauce, and mozzarella cheese, creating a savory and flavorful baked dish.";
        var price = 21.37;
        
        // Act
        var menuitem = new MenuItem(name, description, price);
        
        // Assert
        menuitem.Name.ShouldBe(name);
        menuitem.Description.ShouldBe(description);
        menuitem.Price.ShouldBe(price);
    }

    [Fact]
    public void given_restaurant_should_add_menu_items_to_restaurant_collection()
    {
        // Arrange
        MenuItem[] menuItems =
        [
            new MenuItem("Carbonara"),
            new MenuItem("Lasagne")
        ];
        
        // Act
        _restaurant.AddMenuItems(menuItems);
        
        // Assert
        _restaurant.MenuItems.ShouldNotBeEmpty();
        _restaurant.MenuItems.Count().ShouldBe(menuItems.Length);
        _restaurant.MenuItems.ShouldContain(menuItems.First());
    }


    private readonly Restaurant _restaurant;

    // Setup
    public Restaurant_Tests()
    {
        var id = "1234";
        var name = "Pasta Italiano";
        var description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;
        var address = new Address("Warsaw", "Złota", "1", "00-000", "Poland", 0, 0);
        _restaurant = Restaurant.Create(id, name, description, cuisine, address, null);
    }
}