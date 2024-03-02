namespace PathPilot.Shared.Abstractions.Kernel.Types;

public sealed class EntityId(string value) : TypeId(value)
{
    public static implicit operator EntityId(string value) => new(value);
    public static implicit operator string(EntityId entityId) => entityId.Value;
}