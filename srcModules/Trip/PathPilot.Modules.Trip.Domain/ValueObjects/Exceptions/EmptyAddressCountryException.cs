using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressCountryException()
    : PathPilotException("Address defines empty country.")
{
    
}