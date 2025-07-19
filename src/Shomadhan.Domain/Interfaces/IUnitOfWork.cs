namespace Shomadhan.Domain.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IShopRepository ShopRepository { get; }
    IRoleRepository RoleRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
