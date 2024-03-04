using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressBuildingNumberException()
    : PathPilotException("Address defines empty building number")
{
    
}