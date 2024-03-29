﻿using PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodations.ValueObjects;

public record AccommodationDescription
{
    public string Value { get; private set; }

    public AccommodationDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyAccommodationDescriptionException();
        
        Value = value;
    }

    public static implicit operator string(AccommodationDescription restaurantDescription) => restaurantDescription.Value;
    public static implicit operator AccommodationDescription(string value) => new(value);
}