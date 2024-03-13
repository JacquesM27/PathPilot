using MongoDB.Bson.Serialization.Attributes;

namespace PathPilot.Modules.Trips.Infrastructure.Shared.MongoDb.Mappings;

internal sealed class AddressDocument
{
    public string City { get; set; }
    public string Street { get; set; }
    public string BuildingNumber { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
    public LocationDocument? Location { get; set; }
}

internal sealed class LocationDocument
{
    [BsonElement]
    [BsonDefaultValue("Point")]
    public string Type => "Point";

    public decimal?[] Coordinates { get; set; }
}