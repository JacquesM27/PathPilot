using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.ValueObjects.Exceptions;

public sealed class EmptyAddressStreetException()
    : PathPilotException("Address defines empty street.")
{
}