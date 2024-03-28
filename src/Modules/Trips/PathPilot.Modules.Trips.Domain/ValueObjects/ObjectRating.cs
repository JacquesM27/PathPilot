using PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

namespace PathPilot.Modules.Trips.Domain.ValueObjects;

public sealed record ObjectRating
{
    private const double Minimum = 0;
    private const double Maximum = 5;
    
    public double AverageRating { get; }
    public uint RatingCount { get; }

    public ObjectRating(double averageRating, uint ratingCount)
    {
        if (averageRating is < Minimum or > Maximum)
            throw new InvalidAverageRatingAmountException(averageRating, Minimum, Maximum);
        AverageRating = averageRating;
        RatingCount = ratingCount;
    }
}