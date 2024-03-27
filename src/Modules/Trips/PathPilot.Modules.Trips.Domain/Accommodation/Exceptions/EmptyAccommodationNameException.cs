using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

public sealed class EmptyAccommodationNameException()
    : PathPilotException("Accommodation defines empty name.");