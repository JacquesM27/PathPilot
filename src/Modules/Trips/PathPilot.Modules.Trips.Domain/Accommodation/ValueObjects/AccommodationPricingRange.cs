namespace PathPilot.Modules.Trips.Domain.Accommodation.ValueObjects;

public sealed record AccommodationPricingRange
{
    public decimal From { get; private set; }
    public decimal To { get; private set; }
    public string Currency { get; private set; }
}