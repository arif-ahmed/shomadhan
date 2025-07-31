using Somadhan.Domain.Interfaces;
using Somadhan.Persistence.EF.Data;
using Somadhan.Persistence.EF.Repositories;

namespace Somadhan.Infrastructure.Repositories;
public class ShopRepository : EfRepository<Shop>, IShopRepository
{
    public ShopRepository(AppDbContext context) : base(context)
    {
    }
}
