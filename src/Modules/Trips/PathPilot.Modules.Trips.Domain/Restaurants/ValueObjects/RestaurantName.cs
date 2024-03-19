using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

public sealed record RestaurantName
{
    public string Value { get; private set; }

    public RestaurantName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyRestaurantNameException();
        
        Value = value;
    }

    public static implicit operator string(RestaurantName restaurantName) => restaurantName.Value;
    public static implicit operator RestaurantName(string value) => new(value);
}
