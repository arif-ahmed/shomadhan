using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using Somadhan.Persistence.EF.Data;
using Somadhan.Persistence.EF.Repositories;

namespace Somadhan.Infrastructure.Repositories;
public class BrandRepository : EfRepository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }
}
