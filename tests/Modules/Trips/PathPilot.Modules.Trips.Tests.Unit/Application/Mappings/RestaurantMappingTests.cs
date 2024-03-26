using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.ValueObjects;
using PathPilot.Modules.Trips.Domain.ValueObjects;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Mappings;

public class RestaurantMappingTests
{
    [Fact]
        public void ToDto_Should_Map_Restaurant_To_RestaurantDto()
        {
            // Arrange
            var restaurant = Restaurant.Create("Restaurant Name", "Restaurant Description", "Italian", Guid.NewGuid());

            // Act
            var dto = restaurant.ToDto();

            // Assert
            dto.Id.ShouldBe(restaurant.Id.Value);
            dto.Name.ShouldBe(restaurant.Name);
            dto.Description.ShouldBe(restaurant.Description);
            dto.IsOpened.ShouldBe(restaurant.IsOpened);
            dto.AverageRate.ShouldBe(restaurant.AverageRate);
            dto.CuisineType.ShouldBe(restaurant.CuisineType);
        }

        [Fact]
        public void ToDetailsDto_Should_Map_Restaurant_To_RestaurantDetailsDto_With_Null_Address_And_MenuItems()
        {
            // Arrange
            var restaurant = Restaurant.Create("Restaurant Name", "Restaurant Description", "Italian", Guid.NewGuid());

            // Act
            var dto = restaurant.ToDetailsDto();

            // Assert
            dto.Id.ShouldBe(restaurant.Id.Value);
            dto.Name.ShouldBe(restaurant.Name);
            dto.Description.ShouldBe(restaurant.Description);
            dto.IsOpened.ShouldBe(restaurant.IsOpened);
            dto.AverageRate.ShouldBe(restaurant.AverageRate);
            dto.CuisineType.ShouldBe(restaurant.CuisineType);
            dto.Address.ShouldBeNull();
            dto.MenuItems.ShouldBeEmpty();
        }

        [Fact]
        public void ToDetailsDto_Should_Map_Restaurant_To_RestaurantDetailsDto_With_Address_And_MenuItems()
        {
            // Arrange
            var address = new Address("City", "Street", "1", "12345", "Country", 12.34m, 56.78m);
            var menuItems = new HashSet<MenuItem>
            {
                new("Pizza", "Delicious pizza", 10.99),
                new("Burger", "Tasty burger", 8.99)
            };
            var restaurant = Restaurant.CreateDetailed("Restaurant Name", "Restaurant Description", "Italian", Guid.NewGuid(), address, menuItems);

            // Act
            var dto = restaurant.ToDetailsDto();

            // Assert
            dto.Id.ShouldBe(restaurant.Id.Value);
            dto.Name.ShouldBe(restaurant.Name);
            dto.Description.ShouldBe(restaurant.Description);
            dto.IsOpened.ShouldBe(restaurant.IsOpened);
            dto.AverageRate.ShouldBe(restaurant.AverageRate);
            dto.CuisineType.ShouldBe(restaurant.CuisineType);
            dto.Address.City.ShouldBe(restaurant.Address.City);
            dto.Address.Street.ShouldBe(restaurant.Address.Street);
            dto.Address.BuildingNumber.ShouldBe(restaurant.Address.BuildingNumber);
            dto.Address.PostCode.ShouldBe(restaurant.Address.PostCode);
            dto.Address.Country.ShouldBe(restaurant.Address.Country);
            dto.Address.Longitude.ShouldBe(restaurant.Address.Longitude);
            dto.Address.Latitude.ShouldBe(restaurant.Address.Latitude);
            dto.MenuItems.Count().ShouldBe(restaurant.MenuItems.Count());
            foreach (var menuItem in dto.MenuItems)
            {
                var correspondingMenuItem = restaurant.MenuItems.FirstOrDefault(m => m.Name == menuItem.Name);
                correspondingMenuItem.ShouldNotBeNull();
                menuItem.Description.ShouldBe(correspondingMenuItem.Description);
                menuItem.Price.ShouldBe(correspondingMenuItem.Price);
            }
        }
}