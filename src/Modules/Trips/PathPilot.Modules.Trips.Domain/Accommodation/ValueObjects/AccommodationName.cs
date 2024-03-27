﻿using PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodation.ValueObjects;

public sealed record AccommodationName
{
    public string Value { get; private set; }

    public AccommodationName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyAccommodationNameException();
        
        Value = value;
    }
    
    public static implicit operator string(AccommodationName restaurantName) => restaurantName.Value;
    public static implicit operator AccommodationName(string value) => new(value);
}