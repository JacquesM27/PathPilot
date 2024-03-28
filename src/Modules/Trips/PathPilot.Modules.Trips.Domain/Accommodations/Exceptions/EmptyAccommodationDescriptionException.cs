using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodations.Exceptions;

public sealed class EmptyAccommodationDescriptionException()
    : PathPilotException("Restaurant defines empty description");
