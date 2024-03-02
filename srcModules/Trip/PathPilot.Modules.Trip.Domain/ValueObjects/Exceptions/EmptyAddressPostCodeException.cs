using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressPostCodeException()
    : PathPilotException("Address defines empty post code.")
{
    
}