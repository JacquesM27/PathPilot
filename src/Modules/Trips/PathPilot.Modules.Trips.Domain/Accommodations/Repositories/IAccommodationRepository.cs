using PathPilot.Modules.Trips.Domain.Accommodations.Entities;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Trips.Domain.Accommodations.Repositories;

public interface IAccommodationRepository
{
    Task<IEnumerable<Accommodation>> BrowseAsync();
    Task<IEnumerable<Accommodation>> BrowseAsync(IEnumerable<EntityId> ids);
    Task<Accommodation?> GetAsync(EntityId id);
    Task AddAsync(Accommodation accommodation);
    Task UpdateAsync(Accommodation accommodation);
}