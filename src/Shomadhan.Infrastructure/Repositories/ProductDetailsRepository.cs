using Shomadhan.Domain.Interfaces;
using Shomadhan.Domain.Modules.Product;
using Shomadhan.Infrastructure.Data;

namespace Shomadhan.Infrastructure.Repositories;

public class ProductDetailsRepository : EntityRepository<ProductDetails>, IProductDetailsRepository
{
    public ProductDetailsRepository(AppDbContext context) : base(context)
    {

    }
}
