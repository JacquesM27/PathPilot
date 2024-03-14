using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Infrastructure.Shared.MongoDb.Mappings;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

internal static class RestaurantMappings
{
    internal static RestaurantDocument ToDocuments(this Restaurant restaurant)
        => new()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            IsOpened = restaurant.IsOpened,
            AverageRate = restaurant.AverageRate,
            CuisineType = restaurant.CuisineType,
            Address = restaurant.Address.ToDocument(),
            MenuItems = restaurant.MenuItems.ToDocuments().ToList()
        };
    

    private static IEnumerable<MenuItemDocument> ToDocuments(this IEnumerable<MenuItem> menuItems)
    {
        var menuItemDocuments = menuItems is null ? [] : menuItems.ToArray();
        
        return menuItemDocuments.Length == 0 
            ? Enumerable.Empty<MenuItemDocument>() 
            : menuItemDocuments.Select(x => new MenuItemDocument { Name = x.Name, 
                Description = x.Description, Price = x.Price });
    }


    internal static Restaurant FromDocument(this RestaurantDocument document)
        => new(
            document.Id,
            document.Name,
            document.Description,
            document.IsOpened,
            document.AverageRate,
            document.CuisineType,
            document.Address.FromDocument(),
            document.MenuItems.FromDocument()
        );
    

    private static IEnumerable<MenuItem> FromDocument(this IEnumerable<MenuItemDocument> menuItems)
    {
        var menuItemDocuments = menuItems is null ? [] : menuItems.ToArray();
        
        return menuItemDocuments.Length == 0 
            ? Enumerable.Empty<MenuItem>() 
            : menuItemDocuments.Select(x => new MenuItem(x.Name, x.Description,x.Price ));
    }
}