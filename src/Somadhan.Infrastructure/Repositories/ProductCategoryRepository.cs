using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using Somadhan.Infrastructure.Data;

namespace Somadhan.Infrastructure.Repositories;

public class ProductCategoryRepository : EntityRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context)
    {

    }
}
