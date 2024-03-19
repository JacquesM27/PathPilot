using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Entities;

public sealed class Restaurant
{
    public EntityId Id { get; private set; }
    public RestaurantName Name { get; private set; }
    public RestaurantDescription Description { get; private set; }
    public bool IsOpened { get; private set; }
    public double AverageRate { get; private set; }
    public CuisineType CuisineType { get; private set; }
    public Address? Address { get; private set; }
    public IEnumerable<MenuItem> MenuItems => _menuItems;
    private readonly HashSet<MenuItem> _menuItems = [];

    public Restaurant(EntityId id, RestaurantName name, RestaurantDescription description, 
        bool isOpened, double averageRate, CuisineType cuisineType, Address? address = null, IEnumerable<MenuItem>? menuItems = null)
    {
        Id = id;
        Name = name;
        Description = description;
        IsOpened = isOpened;
        AverageRate = averageRate;
        CuisineType = cuisineType;
        Address = address;
        UpdateMenu(menuItems ?? []);
    }

    public static Restaurant Create(string name, string description,
        string cuisineType)
        => new(Guid.NewGuid(), name, description, true, 0,
            cuisineType);

    public static Restaurant CreateDetailed(string name, string description,
        string cuisineType, Address address, IEnumerable<MenuItem> menuItems)
        => new(Guid.NewGuid(), name, description,true, 0,
        cuisineType, address, menuItems);

    public void UpdateMenu(IEnumerable<MenuItem> menuItems)
    {
        _menuItems.Clear();
        
        foreach (var menuItem in menuItems)
        {
            _menuItems.Add(menuItem);
        }
    }

    public void UpdateAddress(Address address)
        => Address = address ?? throw new MissingRestaurantAddressException();
    
    public void Close() => IsOpened = false;

    public void Open() => IsOpened = true;
}