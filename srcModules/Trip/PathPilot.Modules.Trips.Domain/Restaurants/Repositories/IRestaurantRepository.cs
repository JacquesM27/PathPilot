using PathPilot.Modules.Trips.Domain.Restaurants.Entities;

namespace PathPilot.Modules.Trips.Domain.Restaurants.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> BrowseAsync();
    Task<Restaurant> GetAsync(string id);
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);
}