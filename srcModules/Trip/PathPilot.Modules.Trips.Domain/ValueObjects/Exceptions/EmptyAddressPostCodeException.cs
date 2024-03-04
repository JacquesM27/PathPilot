using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressPostCodeException()
    : PathPilotException("Address defines empty post code.")
{
    
}