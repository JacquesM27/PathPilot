using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> BrowseAsync();
    Task<IEnumerable<Restaurant>> BrowseAsync(IEnumerable<EntityId> ids);
    Task<Restaurant?> GetAsync(EntityId id);
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
}