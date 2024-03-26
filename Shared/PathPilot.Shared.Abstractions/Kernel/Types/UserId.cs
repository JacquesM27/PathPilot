namespace PathPilot.Shared.Abstractions.Kernel.Types;

public sealed class UserId(Guid value) : TypeId(value)
{
    public static implicit operator UserId(Guid value) => new(value);
    public static implicit operator Guid(UserId entityId) => entityId.Value;
}