using System.Linq.Expressions;

namespace Somadhan.Domain.Interfaces;
public interface IEntityRepository<TEntity>
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<(IEnumerable<TEntity>, int)> FindAsync(Expression<Func<TEntity, bool>>? predicate = null, int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<TEntity>, int)> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? query = null, CancellationToken cancellationToken = default);
}
