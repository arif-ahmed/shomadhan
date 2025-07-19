using Microsoft.EntityFrameworkCore;
using Shomadhan.Domain;
using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure.Data;
using System.Linq.Expressions;

namespace Shomadhan.Infrastructure.Repositories;
public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : EntityBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EntityRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(_dbSet));
    }

    public virtual async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be null or empty.");
        }
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
        return entity;
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync();
    }
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        // Find key property info safely
        var entityType = _context.Model.FindEntityType(typeof(TEntity));
        if (entityType == null)
            throw new InvalidOperationException($"Could not find entity type {typeof(TEntity).Name} in the context.");

        var keyProperty = entityType.FindPrimaryKey()?.Properties?.FirstOrDefault();
        if (keyProperty == null)
            throw new InvalidOperationException($"No primary key defined for entity type {typeof(TEntity).Name}.");

        var keyName = keyProperty.Name;
        var keyPropInfo = typeof(TEntity).GetProperty(keyName);
        if (keyPropInfo == null)
            throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} does not have a property named {keyName}.");

        var keyValue = keyPropInfo.GetValue(entity);
        if (keyValue == null)
            throw new InvalidOperationException("Key value cannot be null.");

        // Retrieve the existing entity from the database
        var existingEntity = await _dbSet.FindAsync(keyValue);
        if (existingEntity == null)
            throw new InvalidOperationException("Entity not found in database.");

        // Iterate over properties and update values
        foreach (var property in typeof(TEntity).GetProperties())
        {
            // Skip key and non-writable properties
            if (property.Name == keyName || !property.CanWrite)
                continue;

            var newValue = property.GetValue(entity);
            var oldValue = property.GetValue(existingEntity);

            // Only update if different
            if (!Equals(newValue, oldValue))
            {
                property.SetValue(existingEntity, newValue);
            }
        }
    }
    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be null or empty.");
        }
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");
        }

        return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id, cancellationToken);
    }
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, string sortBy, string sortOrder, int offset = 0, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        await Task.Delay(1000, cancellationToken); // Simulate async operation
        throw new NotImplementedException();
    }
    public virtual async Task<(IEnumerable<TEntity>, int)> FindAsync(Expression<Func<TEntity, bool>> predicate, int offset, int page, CancellationToken cancellationToken = default)
    {
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
        }

        var query = _dbSet.Where(predicate);

        //if (!string.IsNullOrEmpty(sortBy))
        //{
        //    query = sortOrder.ToLower() == "desc" ?
        //        query.OrderByDescending(e => EF.Property<object>(e, sortBy)) :
        //        query.OrderBy(e => EF.Property<object>(e, sortBy));
        //}

        int totalCount = await query.CountAsync();

        if (offset > 0)
        {
            query = query.Skip(offset - 1);
        }
        if (page > 0)
        {
            query = query.Take(page);
        }

        return (await query.ToListAsync(cancellationToken), totalCount);
    }
}
