using PathPilot.Shared.Abstractions.Messaging;

namespace PathPilot.Shared.Abstractions.Events;

/// <summary>
/// Marker interface for handling events <see cref="IEventHandler{TEvent}"/>
/// </summary>
public interface IEvent : IMessage
{
    
}