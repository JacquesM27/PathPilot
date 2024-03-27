using PathPilot.Modules.Trips.Domain.Accommodation.ValueObjects;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Domain.Accommodation.Entities;

public sealed class Accommodation
{
    public EntityId Id { get; private set; }
    public AccommodationName Name { get; private set; }
    public AccommodationDescription Description { get; private set; }
    public AccommodationPricingRange PricingRange { get; private set; }
    public bool IsOpened { get; private set; }
    public double AverageRating { get; private set; }
    public int RatingCount { get; private set; }
    public UserId Owner { get; private set; }
    public Address? Address { get; private set; }
}