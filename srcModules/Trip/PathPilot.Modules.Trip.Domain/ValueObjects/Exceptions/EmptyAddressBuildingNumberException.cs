using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trip.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressBuildingNumberException()
    : PathPilotException("Address defines empty building number")
{
    
}