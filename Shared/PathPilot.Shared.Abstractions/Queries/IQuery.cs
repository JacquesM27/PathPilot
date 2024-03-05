namespace PathPilot.Shared.Abstractions.Queries;

/// <summary>
/// Marker interface for handling queries <see cref="IQueryHandler"/>
/// </summary>
public interface IQuery
{
    
}

public interface IQuery<T> : IQuery
{
    
}