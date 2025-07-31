using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using Somadhan.Persistence.EF.Data;
using Somadhan.Persistence.EF.Repositories;

namespace Somadhan.Infrastructure.Repositories;

public class ProductCategoryRepository : EfRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context)
    {

    }
}
