using PathPilot.Modules.Trip.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trip.Domain.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trip.Domain.Restaurants.Entities;

public sealed class Restaurant
{
    public EntityId Id { get; private set; }
    public RestaurantName Name { get; private set; }
    public RestaurantDescription Description { get; private set; }
    public bool IsOpened { get; private set; }
    public double AverageRate { get; private set; }
    public IEnumerable<MenuItem> MenuItems => _menuItems;
    private readonly IEnumerable<MenuItem> _menuItems;

    public Address Address { get; private set; }
    //CuisineType

    public Restaurant(string id, string name, string description, bool isOpened, double averageRate, IEnumerable<MenuItem> menuItems, Address address)
    {
        Id = id;
        Name = name;
        Description = description;
        IsOpened = isOpened;
        AverageRate = averageRate;
        _menuItems = menuItems;
        Address = address;
    }
}