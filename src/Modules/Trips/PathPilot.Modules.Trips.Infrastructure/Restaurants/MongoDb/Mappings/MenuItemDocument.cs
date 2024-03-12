namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

public class MenuItemDocument
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
}