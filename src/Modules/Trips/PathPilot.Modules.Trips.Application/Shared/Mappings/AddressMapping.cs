using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Domain.ValueObjects;

namespace PathPilot.Modules.Trips.Application.Shared.Mappings;

internal static class AddressMapping
{
    internal static AddressDto? ToDto(this Address? address)
        => address is null 
            ? null 
            : new AddressDto(address.City, address.Street, address.BuildingNumber, address.PostCode,
                address.Country, address.Longitude, address.Latitude);
}