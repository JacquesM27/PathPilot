using PathPilot.Modules.Trip.Domain.ValueObjects.Exceptions;

namespace PathPilot.Modules.Trip.Domain.ValueObjects;

public sealed class Address
{
    public string City { get; private set; }
    public string Street { get; private set; }
    public string BuildingNumber { get; private set; }
    public string PostCode { get; private set; }
    public string Country { get; private set; }
    public decimal Longitude { get; private set; }
    public decimal Latitude { get; private set; }

    public Address(string city, string street, string buildingNumber, string postCode, string country, decimal longitude, decimal latitude)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new EmptyAddressCityException();
        if (string.IsNullOrWhiteSpace(street))
            throw new EmptyAddressStreetException();
        if (string.IsNullOrWhiteSpace(buildingNumber))
            throw new EmptyAddressBuildingNumberException();
        if (string.IsNullOrWhiteSpace(postCode))
            throw new EmptyAddressPostCodeException();
        if (string.IsNullOrWhiteSpace(country))
            throw new EmptyAddressCountryException();

        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        PostCode = postCode;
        Country = country;
        Longitude = longitude;
        Latitude = latitude;
    }
}