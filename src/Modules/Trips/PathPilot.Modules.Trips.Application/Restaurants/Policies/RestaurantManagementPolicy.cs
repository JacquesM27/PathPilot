using PathPilot.Modules.Trips.Domain.Restaurants.Entities;

namespace PathPilot.Modules.Trips.Application.Restaurants.Policies;

internal sealed class RestaurantManagementPolicy : IRestaurantManagementPolicy
{
    public bool CanManageRestaurant(Guid userId, Restaurant restaurant)
    {
        if (userId == Guid.Empty || restaurant.Owner.Value == Guid.Empty)
            return false;
        return userId == restaurant.Owner.Value;
    }
}