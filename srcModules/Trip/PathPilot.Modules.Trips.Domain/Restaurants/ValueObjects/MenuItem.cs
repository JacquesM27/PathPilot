using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

public sealed record MenuItem
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public double? Price { get; set; }

    public MenuItem(string name, string? description = null, double? price = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyMenuItemNameException();
        
        Name = name;
        Description = description;
        Price = price;
    }
    
}