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
            Assert.Equal(address.City, document.City);
            Assert.Equal(address.Street, document.Street);
            Assert.Equal(address.BuildingNumber, document.BuildingNumber);
            Assert.Equal(address.PostCode, document.PostCode);
            Assert.Equal(address.Country, document.Country);
            Assert.NotNull(document.Location);
            Assert.Equal("Point", document.Location.Type);
            Assert.Equal(1.234567m, document.Location.Coordinates[0]);
            Assert.Equal(5.678912m, document.Location.Coordinates[1]);
        }

        [Fact]
        public void ToDocument_ShouldMapAddressToAddressDocument_WithoutLocation()
        {
            // Arrange
            var address = new Address("City", "Street", "123", "12345", "Country");

            // Act
            var document = address.ToDocument();

            // Assert
            Assert.Equal(address.City, document.City);
            Assert.Equal(address.Street, document.Street);
            Assert.Equal(address.BuildingNumber, document.BuildingNumber);
            Assert.Equal(address.PostCode, document.PostCode);
            Assert.Equal(address.Country, document.Country);
            Assert.Null(document.Location);
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
            Assert.Equal(document.City, address.City);
            Assert.Equal(document.Street, address.Street);
            Assert.Equal(document.BuildingNumber, address.BuildingNumber);
            Assert.Equal(document.PostCode, address.PostCode);
            Assert.Equal(document.Country, address.Country);
            Assert.NotNull(address.Longitude);
            Assert.NotNull(address.Latitude);
            Assert.Equal(1.234567m, address.Longitude);
            Assert.Equal(5.678912m, address.Latitude);
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
            Assert.Equal(document.City, address.City);
            Assert.Equal(document.Street, address.Street);
            Assert.Equal(document.BuildingNumber, address.BuildingNumber);
            Assert.Equal(document.PostCode, address.PostCode);
            Assert.Equal(document.Country, address.Country);
            Assert.Null(address.Longitude);
            Assert.Null(address.Latitude);
        }
}