namespace PathPilot.Modules.Trips.Application.Restaurants.DTO;

public sealed record RestaurantDetailsDto(Guid Id, string Name, string Description, bool IsOpened, double AverageRate,
    string CuisineType, AddressDto? Address, IEnumerable<MenuItemDto> MenuItems);