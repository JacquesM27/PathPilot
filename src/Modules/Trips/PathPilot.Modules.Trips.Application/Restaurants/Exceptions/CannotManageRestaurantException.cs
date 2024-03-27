using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Application.Restaurants.Exceptions;

public sealed class CannotManageRestaurantException(Guid userId)
    : PathPilotException($"User with Id: '{userId:D}' does not have permission to edit restaurant")
{
    public Guid UserId => userId;
}