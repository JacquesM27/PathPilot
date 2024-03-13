namespace PathPilot.Shared.Abstractions.Kernel.Types;

public sealed class EntityId(Guid value) : TypeId(value)
{
    public static implicit operator EntityId(Guid value) => new(value);
    public static implicit operator Guid(EntityId entityId) => entityId.Value;
}