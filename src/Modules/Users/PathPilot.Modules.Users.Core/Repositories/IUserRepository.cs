using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(EntityId id);
    Task<User?> GetAsync(Email email);
    Task AddAsync(User user);
}