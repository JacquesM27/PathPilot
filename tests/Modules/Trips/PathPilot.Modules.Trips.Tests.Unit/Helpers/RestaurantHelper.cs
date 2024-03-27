using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

namespace PathPilot.Modules.Trips.Domain.Tests.Helpers;

internal static class RestaurantHelper
{
    internal static Guid OwnerId = Guid.NewGuid();
    internal static Restaurant GetRestaurant()
    {
        const string name = "Pasta Italiano";
        const string description = "Description of the restaurant";
        var cuisine = CuisineType.Italian;
        var restaurant = Restaurant.Create(name, description, cuisine, OwnerId);
        return restaurant;
    }

    internal static IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>
        {
            GetRestaurant(),
            Restaurant.Create("Burger Palace", "Home of delicious burgers", CuisineType.Chinese, OwnerId),
            Restaurant.Create("Sushi Delight", "Authentic Japanese sushi experience", CuisineType.Polish, OwnerId)
        };

        return restaurants;
    }
}