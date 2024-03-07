namespace PathPilot.Modules.Trips.Application.Restaurants.DTO;

public sealed record AddressDto(string City, string Street, string BuildingNumber, string PostCode, 
    string Country, decimal? Longitude = null, decimal? Latitude = null);