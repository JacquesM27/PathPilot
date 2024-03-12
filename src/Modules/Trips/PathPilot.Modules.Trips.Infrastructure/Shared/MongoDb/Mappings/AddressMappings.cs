using PathPilot.Modules.Trips.Domain.ValueObjects;

namespace PathPilot.Modules.Trips.Infrastructure.Shared.MongoDb.Mappings;

internal static class AddressMappings
{
    internal static AddressDocument ToDocument(this Address address)
    {
        LocationDocument? location = null;
        if (address is { Longitude: not null, Latitude: not null })
            location = new LocationDocument
            {
                Coordinates = [address.Longitude, address.Latitude]
            };
        
        return new()
        {
            City = address.City,
            Street = address.Street,
            BuildingNumber = address.BuildingNumber,
            PostCode = address.PostCode,
            Country = address.Country,
            Location = location
        };
    }

    internal static Address FromDocument(this AddressDocument document)
    {
        var longitude = document.Location is null ? null : document.Location!.Coordinates[0];
        var latitude = document.Location is null ? null : document.Location!.Coordinates[1];
        return new(
            document.City,
            document.Street,
            document.BuildingNumber,
            document.PostCode,
            document.Country,
            longitude,
            latitude);
    }
}