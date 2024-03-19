using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressCityException() 
    : PathPilotException("Address defines empty city.")
{
}