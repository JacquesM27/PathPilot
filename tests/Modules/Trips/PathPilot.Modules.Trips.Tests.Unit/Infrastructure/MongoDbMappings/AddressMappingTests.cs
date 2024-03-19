using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Modules.Trips.Infrastructure.Shared.MongoDb.Mappings;

namespace PathPilot.Modules.Trips.Domain.Tests.Infrastructure.MongoDbMappings;

public class AddressMappingTests
{
    [Fact]
    public void ToDocument_ShouldMapAddressToAddressDocument_WithLocation()
    {
        // Arrange
        var address = new Address("City", "Street", "123", "12345", "Country", 1.234567m, 5.678912m);

        // Act
        var document = address.ToDocument();

        // Assert
        document.ShouldNotBeNull();
        document.City.ShouldBe(address.City);
        document.Street.ShouldBe(address.Street);
        document.BuildingNumber.ShouldBe(address.BuildingNumber);
        document.PostCode.ShouldBe(address.PostCode);
        document.Country.ShouldBe(address.Country);
        document.Location.ShouldNotBeNull();
        document.Location.Type.ShouldBe("Point");
        document.Location.Coordinates[0].ShouldBe(1.234567m);
        document.Location.Coordinates[1].ShouldBe(5.678912m);
    }

    [Fact]
    public void ToDocument_ShouldMapAddressToAddressDocument_WithoutLocation()
    {
        // Arrange
        var address = new Address("City", "Street", "123", "12345", "Country");

        // Act
        var document = address.ToDocument();

        // Assert
        document.ShouldNotBeNull();
        document.City.ShouldBe(address.City);
        document.Street.ShouldBe(address.Street);
        document.BuildingNumber.ShouldBe(address.BuildingNumber);
        document.PostCode.ShouldBe(address.PostCode);
        document.Country.ShouldBe(address.Country);
        document.Location.ShouldBeNull();
    }

    [Fact]
    public void FromDocument_ShouldMapAddressDocumentToAddress_WithLocation()
    {
        // Arrange
        var document = new AddressDocument
        {
            City = "City",
            Street = "Street",
            BuildingNumber = "123",
            PostCode = "12345",
            Country = "Country",
            Location = new LocationDocument
            {
                Coordinates = [ 1.234567m, 5.678912m ]
            }
        };

        // Act
        var address = document.FromDocument();

        // Assert
        address.ShouldNotBeNull();
        address.City.ShouldBe(document.City);
        address.Street.ShouldBe(document.Street);
        address.BuildingNumber.ShouldBe(document.BuildingNumber);
        address.PostCode.ShouldBe(document.PostCode);
        address.Country.ShouldBe(document.Country);
        address.Longitude.ShouldBe(1.234567m);
        address.Latitude.ShouldBe(5.678912m);
    }

    [Fact]
    public void FromDocument_ShouldMapAddressDocumentToAddress_WithoutLocation()
    {
        // Arrange
        var document = new AddressDocument
        {
            City = "City",
            Street = "Street",
            BuildingNumber = "123",
            PostCode = "12345",
            Country = "Country"
        };

        // Act
        var address = document.FromDocument();

        // Assert
        address.ShouldNotBeNull();
        address.City.ShouldBe(document.City);
        address.Street.ShouldBe(document.Street);
        address.BuildingNumber.ShouldBe(document.BuildingNumber);
        address.PostCode.ShouldBe(document.PostCode);
        address.Country.ShouldBe(document.Country);
        address.Longitude.ShouldBeNull();
        address.Latitude.ShouldBeNull();
    }
}