using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Domain.ValueObjects;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;
using PathPilot.Modules.Trips.Infrastructure.Shared.MongoDb.Mappings;
using PathPilot.Shared.Abstractions.Kernel.Types;
using Shouldly;

namespace PathPilot.Modules.Trips.Domain.Tests.Infrastructure.MongoDbMappings
{
    public class RestaurantMappingsTests
    {
        [Fact]
        public void ToDocuments_ShouldMapRestaurantToDocument()
        {
            // Arrange
            var restaurantId = Guid.NewGuid();
            var restaurant = new Restaurant(
                new EntityId(restaurantId),
                new RestaurantName("Restaurant Name"),
                new RestaurantDescription("Restaurant Description"),
                true,
                4.5,
                "Italian",
                new Address("City", "Street", "123", "12345", "Country"),
                new List<MenuItem>
                {
                    new("Pizza", "Delicious Pizza", 9.99),
                    new("Pasta", "Tasty Pasta", 11.99)
                });

            // Act
            var document = restaurant.ToDocuments();

            // Assert
            document.Id.ShouldBe(restaurantId);
            document.Name.ShouldBe("Restaurant Name");
            document.Description.ShouldBe("Restaurant Description");
            document.IsOpened.ShouldBeTrue();
            document.AverageRate.ShouldBe(4.5);
            document.CuisineType.ShouldBe("Italian");
            document.Address.City.ShouldBe("City");
            document.Address.Street.ShouldBe("Street");
            document.Address.BuildingNumber.ShouldBe("123");
            document.Address.PostCode.ShouldBe("12345");
            document.Address.Country.ShouldBe("Country");
            document.MenuItems.Count.ShouldBe(2);
            document.MenuItems[0].Name.ShouldBe("Pizza");
            document.MenuItems[0].Description.ShouldBe("Delicious Pizza");
            document.MenuItems[0].Price.ShouldBe(9.99);
        }

        [Fact]
        public void ToDocuments_ShouldMapNullAddressAndMenuItemsToEmptyList()
        {
            // Arrange
            var restaurant = new Restaurant(
                new EntityId(Guid.NewGuid()),
                new RestaurantName("Restaurant Name"),
                new RestaurantDescription("Restaurant Description"),
                true,
                4.5,
                "Italian");

            // Act
            var document = restaurant.ToDocuments();

            // Assert
            document.Address.ShouldBeNull();
            document.MenuItems.ShouldNotBeNull();
            document.MenuItems.ShouldBeEmpty();
        }

        [Fact]
        public void FromDocument_ShouldMapDocumentToRestaurant()
        {
            // Arrange
            var document = new RestaurantDocument
            {
                Id = Guid.NewGuid(),
                Name = "Restaurant Name",
                Description = "Restaurant Description",
                IsOpened = true,
                AverageRate = 4.5,
                CuisineType = "Italian",
                Address = new AddressDocument
                {
                    City = "City",
                    Street = "Street",
                    BuildingNumber = "123",
                    PostCode = "12345",
                    Country = "Country"
                },
                MenuItems =
                [
                    new MenuItemDocument { Name = "Pizza", Description = "Delicious Pizza", Price = 9.99 },
                    new MenuItemDocument { Name = "Pasta", Description = "Tasty Pasta", Price = 11.99 }
                ]
            };

            // Act
            var restaurant = document.FromDocument();

            // Assert
            restaurant.Id.Value.ShouldBe(document.Id);
            restaurant.Name.ShouldBe(new RestaurantName("Restaurant Name"));
            restaurant.Description.ShouldBe(new RestaurantDescription("Restaurant Description"));
            restaurant.IsOpened.ShouldBeTrue();
            restaurant.AverageRate.ShouldBe(4.5);
            restaurant.CuisineType.Value.ShouldBe("Italian");
            restaurant.Address.ShouldNotBeNull();
            restaurant.Address.ShouldBeOfType<Address>();
            restaurant.Address.City.ShouldBe("City");
            restaurant.Address.Street.ShouldBe("Street");
            restaurant.Address.BuildingNumber.ShouldBe("123");
            restaurant.Address.PostCode.ShouldBe("12345");
            restaurant.Address.Country.ShouldBe("Country");
            restaurant.MenuItems.ShouldNotBeNull();
            restaurant.MenuItems.Count().ShouldBe(2);
            restaurant.MenuItems.First().Name.ShouldBe(new MenuItem("Pizza", "Delicious Pizza", 9.99).Name);
            restaurant.MenuItems.First().Description.ShouldBe(new MenuItem("Pizza", "Delicious Pizza", 9.99).Description);
            restaurant.MenuItems.First().Price.ShouldBe(new MenuItem("Pizza", "Delicious Pizza", 9.99).Price);
        }

        [Fact]
        public void FromDocument_ShouldMapNullAddressAndMenuItemsToEmptyList()
        {
            // Arrange
            var document = new RestaurantDocument
            {
                Id = Guid.NewGuid(),
                Name = "Restaurant Name",
                Description = "Restaurant Description",
                IsOpened = true,
                AverageRate = 4.5,
                CuisineType = "Italian"
            };

            // Act
            var restaurant = document.FromDocument();

            // Assert
            restaurant.Id.Value.ShouldBe(document.Id);
            restaurant.Address.ShouldBeNull();
            restaurant.MenuItems.ShouldNotBeNull();
            restaurant.MenuItems.ShouldBeEmpty();
        }
    }
}
