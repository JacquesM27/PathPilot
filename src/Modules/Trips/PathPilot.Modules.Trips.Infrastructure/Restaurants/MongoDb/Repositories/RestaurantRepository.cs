using MongoDB.Driver;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;

internal sealed class RestaurantRepository(IMongoDatabase database) : IRestaurantRepository
{
    private readonly IMongoCollection<Restaurant> _collection = database.GetCollection<Restaurant>(RestaurantConfiguration.CollectionName);
    private readonly FilterDefinitionBuilder<Restaurant> _filterBuilder = Builders<Restaurant>.Filter;

    public async Task<IEnumerable<Restaurant>> BrowseAsync() //TODO: add pagination 
        => await _collection.Find(_filterBuilder.Empty).ToListAsync();

    public async Task<IEnumerable<Restaurant>> BrowseAsync(IEnumerable<EntityId> ids)
    {
        var filter = _filterBuilder.In(r => r.Id, ids);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<Restaurant> GetAsync(EntityId id)
    {
        var filter = _filterBuilder.Eq(r => r.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        await _collection.InsertOneAsync(restaurant);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        var filter = _filterBuilder.Eq(r => r.Id, restaurant.Id);
        await _collection.ReplaceOneAsync(filter, restaurant);
    }
}