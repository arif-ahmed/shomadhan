using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure.Data;

namespace Shomadhan.Infrastructure.Repositories;
public class ShopRepository : EntityRepository<Shop>, IShopRepository
{
    public ShopRepository(AppDbContext context) : base(context)
    {
    }
}
