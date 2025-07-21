using Somadhan.Domain.Core.Identity;

namespace Somadhan.Domain.Interfaces;
public interface IUserRepository : IEntityRepository<User>
{
    Task<User> GetByIdAsync(string id);
    Task AddAsync(User user);
}
