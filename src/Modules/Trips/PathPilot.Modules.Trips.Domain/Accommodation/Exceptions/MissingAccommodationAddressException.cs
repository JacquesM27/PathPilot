using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

public sealed class MissingAccommodationAddressException()
    : PathPilotException("Accommodation defines empty address")
{
    
}