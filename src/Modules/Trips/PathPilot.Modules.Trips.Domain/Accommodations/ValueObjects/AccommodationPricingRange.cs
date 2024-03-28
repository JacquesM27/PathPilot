using PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodations.ValueObjects;

public sealed record AccommodationPricingRange
{
    public decimal From { get; private set; }
    public decimal To { get; private set; }
    public string Currency { get; private set; }

    public AccommodationPricingRange(decimal from, decimal to, string currency)
    {
        if (from >= to)
        {
            var exception = InvalidPricingRangeException.FromGtTo(from, to);
            throw exception;
        }

        if (from <= 0)
        {
            var exception = InvalidPricingRangeException.FromLt0(from);
            throw exception;
        }
        if (to <= 0)
        {
            var exception = InvalidPricingRangeException.ToLt0(to);
            throw exception;
        }
        
        From = from;
        To = to;
        Currency = currency;
    }
}