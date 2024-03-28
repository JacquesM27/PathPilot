using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;

public sealed class InvalidPricingRangeException(string message) : PathPilotException(message)
{
    public static InvalidPricingRangeException FromGtTo(decimal from, decimal to) 
        => new(
            $"Accommodation defines invalid pricing range: from '{from}' is greater than to '{to}'");

    public static InvalidPricingRangeException FromLt0(decimal from)
        => new($"Accommodation defines invalid pricing from value: '{from}' - is less than zero.");
    
    public static InvalidPricingRangeException ToLt0(decimal to)
        => new($"Accommodation defines invalid pricing to value: '{to}' - is less than zero.");
}