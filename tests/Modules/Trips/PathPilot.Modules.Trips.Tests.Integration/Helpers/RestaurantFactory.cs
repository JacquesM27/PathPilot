using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Domain.ValueObjects;

namespace PathPilot.Modules.Trips.Tests.Integration.Helpers;

public static class RestaurantFactory
{
    public static List<Restaurant> CreateTwoRestaurants()
    {
        var restaurant1 = Restaurant.CreateDetailed(
            name: "Restaurant 1",
            description: "Description for Restaurant 1",
            cuisineType: "Cuisine Type 1",
            address: new Address("City 1", "Main St 1", "123", "12345", "Country 1"),
            menuItems: new List<MenuItem>
            {
                new MenuItem("Item 1", "Description 1", 10.99),
                new MenuItem("Item 2", "Description 2", 8.99),
                new MenuItem("Item 3", "Description 3", 12.49)
            }
        );

        var restaurant2 = Restaurant.CreateDetailed(
            name: "Restaurant 2",
            description: "Description for Restaurant 2",
            cuisineType: "Cuisine Type 2",
            address: new Address("City 2", "Elm St 2", "456", "54321", "Country 2", 1.2345m, 2.3456m),
            menuItems: new List<MenuItem>
            {
                new MenuItem("Item A", price: 15.99),
                new MenuItem("Item B", "Description B"),
                new MenuItem("Item C")
            }
        );

        return [restaurant1, restaurant2];
    }

    public static Restaurant CreateRestaurant()
    {
        var restaurant = Restaurant.CreateDetailed(
            name: "Restaurant 3",
            description: "Description for Restaurant 3",
            cuisineType: "Cuisine Type 3",
            address: new Address("City 3", "Main St 1", "123", "12345", "Country 1"),
            menuItems: new List<MenuItem>
            {
                new MenuItem("Item 1", "Description 1", 10.99),
                new MenuItem("Item 2", "Description 2", 8.99),
                new MenuItem("Item 3", "Description 3", 12.49)
            }
        );

        return restaurant;
    }
}
