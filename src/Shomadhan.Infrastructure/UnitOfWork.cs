using Microsoft.Extensions.DependencyInjection;
using Shomadhan.Domain;
using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Repositories;

namespace Shomadhan.Infrastructure;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }


    // Expose repositories as properties
    public IShopRepository ShopRepository => GetRepository<IShopRepository, ShopRepository>();
    public IRoleRepository RoleRepository => GetRepository<IRoleRepository, IdentityRoleRepository>();
    public IUserRepository UserRepository => GetRepository<IUserRepository, IdentityUserRepository>();
    public IProductCategoryRepository ProductCategoryRepository => GetRepository<IProductCategoryRepository, ProductCategoryRepository>();


    public int Commit() => _context.SaveChanges();

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    // For generic repositories
    private IEntityRepository<T> GetRepository<T>() where T : EntityBase
    {
        if (_repositories.TryGetValue(typeof(T), out var repo))
            return (IEntityRepository<T>)repo;

        var repository = new EntityRepository<T>(_context);
        _repositories[typeof(T)] = repository;
        return repository;
    }

    // For custom repositories (e.g., ICustomerRepository)
    private TRepo GetRepository<TRepo, TImpl>()
        where TRepo : class
        where TImpl : class, TRepo
    {
        var type = typeof(TRepo);
        if (_repositories.TryGetValue(type, out var repo))
            return (TRepo)repo;

        // Create instance (could use DI/Activator)
        // var repository = (TRepo)Activator.CreateInstance(typeof(TImpl), _context)!;
        var repository = (TRepo)_serviceProvider.GetRequiredService<TRepo>();
        _repositories[type] = repository;
        return repository;
    }
}


