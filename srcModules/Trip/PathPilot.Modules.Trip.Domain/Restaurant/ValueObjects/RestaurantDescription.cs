﻿using PathPilot.Modules.Trip.Domain.Restaurant.Exceptions;

namespace PathPilot.Modules.Trip.Domain.Restaurant.ValueObjects;

public record RestaurantDescription
{
    public string Value { get; private set; }

    public RestaurantDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyRestaurantDescriptionException();
        
        Value = value;
    }

    public static implicit operator string(RestaurantDescription restaurantDescription) => restaurantDescription.Value;
    public static implicit operator RestaurantDescription(string value) => new(value);
}