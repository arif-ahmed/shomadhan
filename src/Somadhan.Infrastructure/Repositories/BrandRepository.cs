using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using Somadhan.Infrastructure.Data;

namespace Somadhan.Infrastructure.Repositories;
public class BrandRepository : EntityRepository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }
}
