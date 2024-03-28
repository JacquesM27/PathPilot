using PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;
using PathPilot.Modules.Trips.Domain.Accommodations.ValueObjects;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Domain.Accommodations.Entities;

public sealed class Accommodation
{
    public EntityId Id { get; private set; }
    public AccommodationName Name { get; private set; }
    public AccommodationDescription Description { get; private set; }
    public NumberOfStars NumberOfStars { get; private set; }
    public bool IsOpened { get; private set; }
    public ObjectRating ObjectRating { get; private set; }
    public UserId Owner { get; private set; }
    public Address? Address { get; private set; }
    public AccommodationPricingRange? PricingRange { get; private set; }

    public Accommodation(EntityId id, AccommodationName name, AccommodationDescription description, 
        NumberOfStars numberOfStars, bool isOpened, ObjectRating objectRating, UserId owner, 
        Address? address = null, AccommodationPricingRange? pricingRange = null)
    {
        Id = id;
        Name = name;
        Description = description;
        NumberOfStars = numberOfStars;
        IsOpened = isOpened;
        ObjectRating = objectRating;
        Owner = owner;
        Address = address;
        PricingRange = pricingRange;
    }

    public static Accommodation Create(string name, string description, int numberOfStars, Guid ownerId) =>
        new(Guid.NewGuid(), name, description, numberOfStars, true, 
            new ObjectRating(0, 0), ownerId);

    public void ChangePricingRange(decimal from, decimal to, string currency)
        => PricingRange = new AccommodationPricingRange(from, to, currency);
    
    public void UpdateAddress(Address address)
        => Address = address ?? throw new MissingAccommodationAddressException();
    
    public void Close() => IsOpened = false;

    public void Open() => IsOpened = true;
}