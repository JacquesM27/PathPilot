using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressCountryException()
    : PathPilotException("Address defines empty country.")
{
    
}