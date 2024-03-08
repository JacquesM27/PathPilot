using MongoDB.Driver;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;

internal sealed class RestaurantRepository(
    IMongoCollection<Restaurant> collection
    ) : IRestaurantRepository
{
    private readonly FilterDefinitionBuilder<Restaurant> _filterBuilder = Builders<Restaurant>.Filter;

    public async Task<IEnumerable<Restaurant>> BrowseAsync() //TODO: add pagination 
        => await collection.Find(_filterBuilder.Empty).ToListAsync();

    public async Task<IEnumerable<Restaurant>> BrowseAsync(IEnumerable<EntityId> ids)
    {
        var filter = _filterBuilder.In(r => r.Id, ids);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task<Restaurant> GetAsync(EntityId id)
    {
        var filter = _filterBuilder.Eq(r => r.Id, id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        await collection.InsertOneAsync(restaurant);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        var filter = _filterBuilder.Eq(r => r.Id, restaurant.Id);
        await collection.ReplaceOneAsync(filter, restaurant);
    }
}