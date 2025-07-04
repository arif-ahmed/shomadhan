using Shomadhan.Domain.Core.Identity;

namespace Shomadhan.Domain.Interfaces;
public interface IUserRepository : IEntityRepository<User>
{
    Task<User> GetByIdAsync(string id);
    Task AddAsync(User user);
}
