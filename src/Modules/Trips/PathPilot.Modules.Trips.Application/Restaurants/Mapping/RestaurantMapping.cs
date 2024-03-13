using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Shared.Mappings;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

namespace PathPilot.Modules.Trips.Application.Restaurants.Mapping;

internal static class RestaurantMapping
{
    internal static RestaurantDetailsDto ToDetailsDto(this Restaurant restaurant)
        => new(restaurant.Id,
            restaurant.Name,
            restaurant.Description,
            restaurant.IsOpened, 
            restaurant.AverageRate, 
            restaurant.CuisineType,
            restaurant.Address?.ToDto(),
            restaurant.MenuItems?.Select(x => x.ToDto()) ?? []);

    internal static RestaurantDto ToDto(this Restaurant restaurant)
        => new(restaurant.Id,
            restaurant.Name,
            restaurant.Description,
            restaurant.IsOpened,
            restaurant.AverageRate,
            restaurant.CuisineType);

    private static MenuItemDto ToDto(this MenuItem menuItem)
        => new(menuItem.Name, menuItem.Description, menuItem.Price);

}