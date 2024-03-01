namespace PathPilot.Shared.Abstractions.Kernel.Types;

public abstract class AggregateRoot<TKey>
{
    public TKey Id { get; protected set; }
}

public abstract class AggregateRoot : AggregateRoot<string>
{
    
}