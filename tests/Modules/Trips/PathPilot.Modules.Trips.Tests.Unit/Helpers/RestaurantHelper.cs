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
        var restaurant = Restaurant.Create(name, description, cuisine);
        return restaurant;
    }
}