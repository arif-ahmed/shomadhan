using System.Linq.Expressions;
using System.Reflection;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Somadhan.Domain;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Persistence.Mongo;
public class MongoRepository<TEntity> : IEntityRepository<TEntity> where TEntity : EntityBase
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoRepository(IMongoDatabase database, string? collectionName = null)
    {
        //_collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);

        // var resolvedName = collectionName ?? GetCollectionNameFromAttribute() ?? typeof(TEntity).Name;
        var resolvedName = collectionName ?? typeof(TEntity).Name + "s";
        _collection = database.GetCollection<TEntity>(resolvedName);
    }

    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<TEntity>, int)> FindAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        int pageNumber = 1,
        int pageSize = 100,
        CancellationToken cancellationToken = default)
    {
        var query = _collection.AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        var totalCount = await query.CountAsync(cancellationToken);
        var results = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (results, totalCount);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var idProperty = typeof(TEntity).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException("Entity must have an 'Id' property.");

        var id = idProperty.GetValue(entity)?.ToString();
        if (string.IsNullOrEmpty(id))
            throw new InvalidOperationException("Entity Id cannot be null or empty.");

        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<(IEnumerable<TEntity>, int)> GetAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? query = null,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = _collection.AsQueryable();

        var finalQuery = query != null ? query(baseQuery) : baseQuery;

        var results = await finalQuery.ToListAsync(cancellationToken);
        return (results, results.Count);
    }


    private string? GetCollectionNameFromAttribute()
    {
        var attr = typeof(TEntity).GetCustomAttribute<MongoCollectionAttribute>();
        return attr?.CollectionName;
    }
}

