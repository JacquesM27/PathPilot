using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;

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
        
        actualRestaurant.Address.ShouldBe(new AddressDto(
            expectedRestaurant.Address.City,
            expectedRestaurant.Address.Street,
            expectedRestaurant.Address.BuildingNumber,
            expectedRestaurant.Address.PostCode,
            expectedRestaurant.Address.Country,
            expectedRestaurant.Address.Longitude,
            expectedRestaurant.Address.Latitude
        ));

        actualRestaurant.MenuItems.ShouldBe(expectedRestaurant.MenuItems.Select(item =>
            new MenuItemDto(item.Name, item.Description, item.Price)
        ));
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
    
    internal static void AssertRestaurantDtoMatchesRestaurant(this RestaurantDetailsDto actualRestaurant, Restaurant expectedRestaurant)
    {
        actualRestaurant.Id.ShouldBe(expectedRestaurant.Id.Value);
        actualRestaurant.Name.ShouldBe(expectedRestaurant.Name);
        actualRestaurant.Description.ShouldBe(expectedRestaurant.Description);
        actualRestaurant.IsOpened.ShouldBe(expectedRestaurant.IsOpened);
        actualRestaurant.AverageRate.ShouldBe(expectedRestaurant.AverageRate);
        actualRestaurant.CuisineType.ShouldBe(expectedRestaurant.CuisineType);
        
        actualRestaurant.Address.ShouldBe(new AddressDto(
            expectedRestaurant.Address.City,
            expectedRestaurant.Address.Street,
            expectedRestaurant.Address.BuildingNumber,
            expectedRestaurant.Address.PostCode,
            expectedRestaurant.Address.Country,
            expectedRestaurant.Address.Longitude,
            expectedRestaurant.Address.Latitude
        ));

        actualRestaurant.MenuItems.ShouldBe(expectedRestaurant.MenuItems.Select(item =>
            new MenuItemDto(item.Name, item.Description, item.Price)
        ));
    }
    
    internal static void AssertRestaurantDtoMatchesRestaurant(this RestaurantDto actualRestaurant, Restaurant expectedRestaurant)
    {
        actualRestaurant.Id.ShouldBe(expectedRestaurant.Id.Value);
        actualRestaurant.Name.ShouldBe(expectedRestaurant.Name);
        actualRestaurant.Description.ShouldBe(expectedRestaurant.Description);
        actualRestaurant.IsOpened.ShouldBe(expectedRestaurant.IsOpened);
        actualRestaurant.AverageRate.ShouldBe(expectedRestaurant.AverageRate);
        actualRestaurant.CuisineType.ShouldBe(expectedRestaurant.CuisineType);
    }
}