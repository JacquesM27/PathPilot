using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Domain.ValueObjects;

namespace PathPilot.Modules.Trips.Application.Restaurants.Mapping;

internal static class RestaurantMapping
{
    internal static RestaurantDto ToDto(this Restaurant restaurant)
        => new(restaurant.Id,
            restaurant.Name,
            restaurant.Description,
            restaurant.IsOpened, restaurant.AverageRate, restaurant.CuisineType,
            restaurant.Address?.ToDto(),
            restaurant.MenuItems.Select(x => x.ToDto())); 

    internal static MenuItemDto ToDto(this MenuItem menuItem)
        => new(menuItem.Name, menuItem.Description, menuItem.Price);

    internal static AddressDto? ToDto(this Address? address)
        => address is null 
            ? null 
            : new AddressDto(address.City, address.Street, address.BuildingNumber, address.PostCode,
            address.Country, address.Longitude, address.Latitude);
}