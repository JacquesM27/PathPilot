using PathPilot.Modules.Trips.Domain.Restaurants.Entities;

namespace PathPilot.Modules.Trips.Application.Restaurants.Policies;

internal interface IRestaurantManagementPolicy
{
    bool CanManageRestaurant(Guid userId, Restaurant restaurant);
}