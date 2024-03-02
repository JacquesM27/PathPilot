namespace PathPilot.Shared.Abstractions.Kernel.Types;

public abstract class TypeId(string value) : IEquatable<TypeId>
{
    public string Value => value;
    
    public bool Equals(TypeId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TypeId)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(TypeId typeId) => typeId.Value;

    public static bool operator ==(TypeId? t1, TypeId? t2)
    {
        if (ReferenceEquals(t1, t2)) return true;
        if (ReferenceEquals(null, t1) || ReferenceEquals(null, t2)) return false;

        return t1!.Value.Equals(t2!.Value);
    }

    public static bool operator !=(TypeId? t1, TypeId? t2) => !(t1 == t2);
}