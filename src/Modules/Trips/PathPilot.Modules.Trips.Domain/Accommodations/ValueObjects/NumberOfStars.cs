using PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodations.ValueObjects;

public sealed record NumberOfStars
{
    private const int Minimum = 1;
    private const int Maximum = 5;
    public int Value { get; set; }

    public NumberOfStars(int value)
    {
        if (value is < Minimum or > Maximum)
            throw new InvalidStarsValueException(value, Minimum, Maximum);
        Value = value;
    }

    public static implicit operator NumberOfStars(int value) => new(value);
    public static implicit operator int(NumberOfStars value) => value.Value;
}