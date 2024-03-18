using PathPilot.Modules.Users.Core.Entities;

namespace PathPilot.Modules.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);
    Task<User?> GetAsync(string email);
    Task AddAsync(User user);
}