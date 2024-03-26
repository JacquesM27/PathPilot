using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

namespace PathPilot.Modules.Trips.Domain.Tests.Helpers;

internal static class RestaurantHelper
{
    internal static Restaurant GetRestaurant()
    {
        const string name = "Pasta Italiano";
        const string description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;
        var restaurant = Restaurant.Create(name, description, cuisine, Guid.NewGuid());
        return restaurant;
    }

    internal static IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>
        {
            GetRestaurant(),
            Restaurant.Create("Burger Palace", "Home of delicious burgers", CuisineType.Chinese, Guid.NewGuid()),
            Restaurant.Create("Sushi Delight", "Authentic Japanese sushi experience", CuisineType.Polish, Guid.NewGuid())
        };

        return restaurants;
    }
}