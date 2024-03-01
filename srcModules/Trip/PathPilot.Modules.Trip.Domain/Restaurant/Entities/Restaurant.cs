using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trip.Domain.Restaurant.Entities;

public class Restaurant : AggregateRoot// TODO: replace with entity id
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsOpened { get; private set; }
    public double AverageRate { get; private set; }
    public IEnumerable<MenuItem> MenuItems => _menuItems;
    private IEnumerable<MenuItem> _menuItems;
    //Address
    //CuisineType

    public Restaurant(string id, string name, string description, bool isOpened, double averageRate, IEnumerable<MenuItem> menuItems)
    {
        Id = id;
        Name = name;
        Description = description;
        IsOpened = isOpened;
        AverageRate = averageRate;
        _menuItems = menuItems;
    }
}