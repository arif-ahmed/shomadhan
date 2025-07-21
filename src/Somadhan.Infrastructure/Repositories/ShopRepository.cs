using Somadhan.Domain.Interfaces;
using Somadhan.Infrastructure.Data;

namespace Somadhan.Infrastructure.Repositories;
public class ShopRepository : EntityRepository<Shop>, IShopRepository
{
    public ShopRepository(AppDbContext context) : base(context)
    {
    }
}
