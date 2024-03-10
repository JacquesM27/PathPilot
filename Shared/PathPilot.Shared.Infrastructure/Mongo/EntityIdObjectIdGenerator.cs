using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Shared.Infrastructure.Mongo;

public sealed class EntityIdObjectIdGenerator : IIdGenerator
{
    public static EntityIdObjectIdGenerator Instance { get; } = new EntityIdObjectIdGenerator();

    public EntityIdObjectIdGenerator()
    {
    }
    
    public object GenerateId(object container, object document)
    {
        return new EntityId(ObjectId.GenerateNewId().ToString());
    }

    public bool IsEmpty(object id)
    {
        if (id is EntityId entityId)
        {
            return string.IsNullOrWhiteSpace(entityId?.Value);
        }
        return false;
    }
}