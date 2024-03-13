namespace PathPilot.Modules.Trips.Application.Restaurants.Commands.Shared;

public sealed record MenuItemRecord(string Name, string? Description = null, double? Price = null);