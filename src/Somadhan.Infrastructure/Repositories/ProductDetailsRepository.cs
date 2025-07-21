using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using Somadhan.Infrastructure.Data;

namespace Somadhan.Infrastructure.Repositories;

public class ProductDetailsRepository : EntityRepository<ProductDetails>, IProductDetailsRepository
{
    public ProductDetailsRepository(AppDbContext context) : base(context)
    {

    }
}
