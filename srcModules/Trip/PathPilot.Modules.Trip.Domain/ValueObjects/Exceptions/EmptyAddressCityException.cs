using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressCityException() 
    : PathPilotException("Address defines empty city.")
{
}