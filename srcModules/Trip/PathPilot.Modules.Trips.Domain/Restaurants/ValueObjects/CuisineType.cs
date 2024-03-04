using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;

public sealed record CuisineType
{
    public static string Italian = nameof(Italian);
    public static string Chinese = nameof(Chinese);
    public static string Polish = nameof(Polish);
    
    public string Value { get; private set; }

    private CuisineType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyRestaurantCuisineTypeException();
        Value = value;
    }

    public static implicit operator CuisineType(string value) => new(value);
    public static implicit operator string(CuisineType cuisineType) => cuisineType.Value;
}