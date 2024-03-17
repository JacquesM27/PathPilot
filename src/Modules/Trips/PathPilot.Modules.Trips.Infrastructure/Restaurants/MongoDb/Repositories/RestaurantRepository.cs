using MongoDB.Driver;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;

internal sealed class RestaurantRepository(RestaurantsMongoContext context) : IRestaurantRepository
{
    private readonly IMongoCollection<RestaurantDocument> _collection = context.Restaurants;
    private readonly FilterDefinitionBuilder<RestaurantDocument> _filterBuilder = 
        Builders<RestaurantDocument>.Filter;

    public async Task<IEnumerable<Restaurant>> BrowseAsync() //TODO: add pagination 
    {
        var documents = await _collection.Find(_filterBuilder.Empty).ToListAsync();
        return documents.Select(x => x.FromDocument()!);
    }

    public async Task<IEnumerable<Restaurant>> BrowseAsync(IEnumerable<EntityId> ids)
    {
        var filter = _filterBuilder.In(r => r.Id, ids.Select(x => x.Value));
        var documents = await _collection.Find(filter).ToListAsync();
        return documents.Where(x => x is not null).Select(x => x.FromDocument()!);
    }

    public async Task<Restaurant?> GetAsync(EntityId id)
    {
        var filter = _filterBuilder.Eq(r => r.Id, id.Value);
        var document = await _collection.Find(filter).FirstOrDefaultAsync();
        return document.FromDocument();
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        var document = restaurant.ToDocument();
        await _collection.InsertOneAsync(document);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        var document = restaurant.ToDocument();
        var filter = _filterBuilder.Eq(r => r.Id, document.Id);
        await _collection.ReplaceOneAsync(filter, document);
    }
}