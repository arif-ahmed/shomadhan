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
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
        }

        if (string.IsNullOrEmpty(sortBy))
        {
            throw new ArgumentNullException(nameof(sortBy), "SortBy cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(sortOrder))
        {
            throw new ArgumentNullException(nameof(sortOrder), "SortOrder cannot be null or empty.");
        }

        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        var query = _dbSet.Where(predicate);
        if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
        {
            query = query.OrderBy(e => EF.Property<object>(e, sortBy));
        }
        else if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
        {
            query = query.OrderByDescending(e => EF.Property<object>(e, sortBy));
        }
        else
        {
            throw new ArgumentException("SortOrder must be either 'asc' or 'desc'.", nameof(sortOrder));
        }

        if (offset > 0)
        {
            query = query.Skip(offset);
        }

        if (pageSize > 0)
        {
            query = query.Take(pageSize);
        }

        var entities = await query.ToListAsync(cancellationToken);
        if (entities == null || !entities.Any())
        {
            throw new KeyNotFoundException($"No entities found matching the criteria.");
        }

        return entities;
    }
    public virtual async Task<(IEnumerable<TEntity>, int)> FindAsync(Expression<Func<TEntity, bool>>? predicate = null, int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default)
    {

        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
        }

        var query = _dbSet.Where(predicate);

        int totalCount = await query.CountAsync();

        if (pageNumber > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize);
        }
        if (pageNumber > 0)
        {
            query = query.Take(pageSize);
        }

        return (await query.ToListAsync(cancellationToken), totalCount);
    }

    public async Task<(IEnumerable<TEntity>, int)> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? query = null, CancellationToken cancellationToken = default)
    {
        var queryable = _dbSet.AsQueryable();

        if (query != null)
        {
            queryable = query(queryable);
        }

        var entities = await queryable.ToListAsync(cancellationToken);
        int count = await queryable.CountAsync(cancellationToken);

        return (entities, count);
    }
}
