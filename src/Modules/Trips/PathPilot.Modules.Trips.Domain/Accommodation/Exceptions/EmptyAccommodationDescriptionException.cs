using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

public sealed class EmptyAccommodationDescriptionException()
    : PathPilotException("Restaurant defines empty description");
