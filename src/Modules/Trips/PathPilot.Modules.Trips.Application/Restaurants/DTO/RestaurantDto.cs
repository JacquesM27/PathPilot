namespace PathPilot.Modules.Trips.Application.Restaurants.DTO;

public sealed record RestaurantDto(string Id, string Name, string Description, bool IsOpened, double AverageRate,
    string CuisineType);