using PathPilot.Modules.Trips.Application.Shared.Mappings;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Mappings;

public class AddressMappingTests
{
    [Fact]
    public void Address_To_AddressDto_Should_Map_Correctly_With_Coordinates()
    {
        // Arrange
        var address = new Address("City", "Street", "1", "12345", "Country", 12.34m, 56.78m);

        // Act
        var dto = address.ToDto();

        // Assert
        dto.City.ShouldBe(address.City);
        dto.Street.ShouldBe(address.Street);
        dto.BuildingNumber.ShouldBe(address.BuildingNumber);
        dto.PostCode.ShouldBe(address.PostCode);
        dto.Country.ShouldBe(address.Country);
        dto.Longitude.ShouldBe(address.Longitude);
        dto.Latitude.ShouldBe(address.Latitude);
    }

    [Fact]
    public void Address_To_AddressDto_Should_Map_Correctly_With_Null_Coordinates()
    {
        // Arrange
        var address = new Address("City", "Street", "1", "12345", "Country", null, null);

        // Act
        var dto = address.ToDto();

        // Assert
        dto.City.ShouldBe(address.City);
        dto.Street.ShouldBe(address.Street);
        dto.BuildingNumber.ShouldBe(address.BuildingNumber);
        dto.PostCode.ShouldBe(address.PostCode);
        dto.Country.ShouldBe(address.Country);
        dto.Longitude.ShouldBeNull();
        dto.Latitude.ShouldBeNull();
    }

    [Fact]
    public void Null_Address_Should_Be_Mapped_To_Null_AddressDto()
    {
        // Arrange
        Address address = null;

        // Act
        var dto = address.ToDto();

        // Assert
        dto.ShouldBeNull();
    }
}