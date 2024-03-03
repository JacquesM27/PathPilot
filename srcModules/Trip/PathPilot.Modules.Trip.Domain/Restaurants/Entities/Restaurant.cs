using System.Collections;
using PathPilot.Modules.Trip.Domain.Restaurants.Exceptions;
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
    public CuisineType CuisineType { get; private set; }
    public Address Address { get; private set; }
    public IEnumerable<MenuItem> MenuItems => _menuItems;
    private readonly IEnumerable<MenuItem> _menuItems;


    private Restaurant(string id, RestaurantName name, RestaurantDescription description, 
        bool isOpened, double averageRate, CuisineType cuisineType, 
        Address address, IEnumerable<MenuItem> menuItems)
    {
        Id = id;
        Name = name;
        Description = description;
        IsOpened = isOpened;
        AverageRate = averageRate;
        CuisineType = cuisineType;

        Address = address ?? throw new MissingRestaurantAddressException();
        _menuItems = menuItems ?? [];
    }

    public static Restaurant Create(string id, string name, string description,
        string cuisineType, Address address, IEnumerable<MenuItem> menuItems)
        => new(id, name, description, true, 0,
            cuisineType, address, menuItems);
    
    //public 
}