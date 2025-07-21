using Somadhan.Domain.Core.Identity;

namespace Somadhan.Domain.Interfaces;
public interface IRoleRepository : IEntityRepository<Role>
{
    // Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Role role);
}
