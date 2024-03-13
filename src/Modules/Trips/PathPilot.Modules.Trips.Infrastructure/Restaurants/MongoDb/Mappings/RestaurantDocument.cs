using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using PathPilot.Modules.Trips.Infrastructure.Shared.MongoDb.Mappings;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

internal sealed class RestaurantDocument
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsOpened { get; set; }
    public double AverageRate { get; set; }
    public string CuisineType { get; set; }
    public AddressDocument Address { get; set; }
    public List<MenuItemDocument> MenuItems { get; set; }
}