using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using Shouldly;

namespace PathPilot.Modules.Trips.Tests.Integration.Helpers;

internal static class RestaurantAssertions
{

    internal static void AssertRestaurantDtoMatchesRestaurant(this Restaurant expectedRestaurant, RestaurantDetailsDto actualRestaurant)
    {
        actualRestaurant.Id.ShouldBe(expectedRestaurant.Id.Value);
        actualRestaurant.Name.ShouldBe(expectedRestaurant.Name);
        actualRestaurant.Description.ShouldBe(expectedRestaurant.Description);
        actualRestaurant.IsOpened.ShouldBe(expectedRestaurant.IsOpened);
        actualRestaurant.AverageRate.ShouldBe(expectedRestaurant.AverageRate);
        actualRestaurant.CuisineType.ShouldBe(expectedRestaurant.CuisineType);
        //add items and address?
    }
    
    internal static void AssertRestaurantDtoMatchesRestaurant(this Restaurant expectedRestaurant, RestaurantDto actualRestaurant)
    {
        actualRestaurant.Id.ShouldBe(expectedRestaurant.Id.Value);
        actualRestaurant.Name.ShouldBe(expectedRestaurant.Name);
        actualRestaurant.Description.ShouldBe(expectedRestaurant.Description);
        actualRestaurant.IsOpened.ShouldBe(expectedRestaurant.IsOpened);
        actualRestaurant.AverageRate.ShouldBe(expectedRestaurant.AverageRate);
        actualRestaurant.CuisineType.ShouldBe(expectedRestaurant.CuisineType);
    }
    
}