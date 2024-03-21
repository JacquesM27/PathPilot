using MongoDB.Driver;
using PathPilot.Modules.Users.Core.DAL.Mappings;
using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.Repositories;
using PathPilot.Modules.Users.Core.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Users.Core.DAL.Repositories;

internal sealed class UserRepository(UsersMongoContext context) : IUserRepository
{
    private readonly IMongoCollection<UserDocument> _collection = context.Users;

    private readonly FilterDefinitionBuilder<UserDocument> _filterBuilder =
        Builders<UserDocument>.Filter;

    public async Task<User?> GetAsync(EntityId id)
    {
        var filter = _filterBuilder.Eq(u => u.Id, id.Value);
        var document = await _collection.Find(filter).FirstOrDefaultAsync();
        return document.FromDocument();
    }

    public async Task<User?> GetAsync(Email email)
    {
        var filter = _filterBuilder.Eq(u => u.Email, email.Value);
        var document = await _collection.Find(filter).FirstOrDefaultAsync();
        return document.FromDocument();
    }

    public async Task AddAsync(User user)
    {
        var document = user.ToDocument();
        await _collection.InsertOneAsync(document);
    }
}