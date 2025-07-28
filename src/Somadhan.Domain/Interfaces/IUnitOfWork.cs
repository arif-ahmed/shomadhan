namespace Somadhan.Domain.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IShopRepository ShopRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    IProductDetailsRepository ProductDetailsRepository { get; }
    IEntityRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
