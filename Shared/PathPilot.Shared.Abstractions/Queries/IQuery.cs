namespace PathPilot.Shared.Abstractions.Queries;

/// <summary>
/// Marker interface for handling queries <see cref="IQueryHandler{TQuery,TResult}"/>
/// </summary>
public interface IQuery
{
    
}

/// <inheritdoc cref="IQuery"/>
public interface IQuery<T> : IQuery
{
    
}