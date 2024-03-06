using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Entities;

public sealed class Restaurant
{
    public EntityId Id { get; }
    public RestaurantName Name { get; private set; }
    public RestaurantDescription Description { get; private set; }
    public bool IsOpened { get; private set; }
    public double AverageRate { get; private set; }
    public CuisineType CuisineType { get; private set; }
    public Address Address { get; private set; }
    public IEnumerable<MenuItem> MenuItems => _menuItems;
    private readonly HashSet<MenuItem> _menuItems = [];

    private Restaurant(RestaurantName name, RestaurantDescription description, 
        bool isOpened, double averageRate, CuisineType cuisineType)
    {
        Name = name;
        Description = description;
        IsOpened = isOpened;
        AverageRate = averageRate;
        CuisineType = cuisineType;
    }

    public static Restaurant Create(string name, string description,
        string cuisineType)
        => new(name, description, true, 0,
            cuisineType);

    public void UpdateMenu(IEnumerable<MenuItem> menuItems)
    {
        foreach (var menuItem in menuItems)
        {
            var existingMenuItem = _menuItems.FirstOrDefault(item => item.Name == menuItem.Name);
        
            if (existingMenuItem != null)
            {
                _menuItems.Remove(existingMenuItem);
                _menuItems.Add(menuItem);
            }
            else
                _menuItems.Add(menuItem);
        }
    }

    public void UpdateAddress(Address address)
        => Address = address ?? throw new MissingRestaurantAddressException();
    
    public void Close() => IsOpened = false;

    public void Open() => IsOpened = true;
}