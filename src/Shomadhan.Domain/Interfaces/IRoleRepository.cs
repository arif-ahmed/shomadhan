using Shomadhan.Domain.Core.Identity;

namespace Shomadhan.Domain.Interfaces;
public interface IRoleRepository : IEntityRepository<Role>
{
    // Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Role role);
}
