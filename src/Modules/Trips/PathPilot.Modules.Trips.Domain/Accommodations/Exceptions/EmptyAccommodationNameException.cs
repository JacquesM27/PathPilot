using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;

public sealed class EmptyAccommodationNameException()
    : PathPilotException("Accommodation defines empty name.");