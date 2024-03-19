using PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;
using PathPilot.Modules.Trips.Application.Restaurants.Mapping;
using PathPilot.Modules.Trips.Domain.Restaurants.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Tests.Application.Mappings;

public class MenuItemMappingTests
{
    [Fact]
        public void MapToMenuItems_Should_Map_MenuItemRecord_To_MenuItem()
        {
            // Arrange
            var menuItemRecord = new MenuItemRecord("Pizza", "Delicious pizza", 10.99);

            // Act
            var menuItem = new List<MenuItemRecord> { menuItemRecord }.MapToMenuItems().Single();

            // Assert
            menuItem.Name.ShouldBe(menuItemRecord.Name);
            menuItem.Description.ShouldBe(menuItemRecord.Description);
            menuItem.Price.ShouldBe(menuItemRecord.Price);
        }

        [Fact]
        public void MapToMenuItems_Should_ThrowException_When_Name_Is_Null_Or_Empty()
        {
            // Arrange
            var menuItemRecords = new List<MenuItemRecord>
            {
                new (null),
                new (""),
                new (" ", "Description", 10.99)
            };
            
            // Act & Assert
            foreach (var menuItemRecord in menuItemRecords)
            {
                Should.Throw<EmptyMenuItemNameException>(() => new List<MenuItemRecord> { menuItemRecord }.MapToMenuItems().ToList());
            }
        }

        [Fact]
        public void MapToMenuItems_Should_Handle_Nullable_Description_And_Price()
        {
            // Arrange
            var menuItemRecord = new MenuItemRecord("Burger");

            // Act
            var menuItem = new List<MenuItemRecord> { menuItemRecord }.MapToMenuItems().Single();

            // Assert
            menuItem.Name.ShouldBe(menuItemRecord.Name);
            menuItem.Description.ShouldBeNull();
            menuItem.Price.ShouldBeNull();
        }

        [Fact]
        public void MapToMenuItems_Should_Handle_Empty_Description_And_Price()
        {
            // Arrange
            var menuItemRecord = new MenuItemRecord("Burger", "", null);

            // Act
            var menuItem = new List<MenuItemRecord> { menuItemRecord }.MapToMenuItems().Single();

            // Assert
            menuItem.Name.ShouldBe(menuItemRecord.Name);
            menuItem.Description.ShouldBe("");
            menuItem.Price.ShouldBeNull();
        }
}