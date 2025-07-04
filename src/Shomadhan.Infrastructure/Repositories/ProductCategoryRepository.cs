using Shomadhan.Domain.Interfaces;
using Shomadhan.Domain.Modules.Product;
using Shomadhan.Infrastructure.Data;

namespace Shomadhan.Infrastructure.Repositories;

public class ProductCategoryRepository : EntityRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context)
    {

    }
}
