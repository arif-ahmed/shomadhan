using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Modules.Product;
using Somadhan.Persistence.EF.Data.EntityConfigurations.Views;

namespace Somadhan.Persistence.EF.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Define DbSets for your entities here
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Role> ApplicationRoles { get; set; }
    public DbSet<User> ApplicationUsers { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductDetails> ProductDetails { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Shop>().ToTable("Shops");
        modelBuilder.Entity<ProductDetails>().ToTable("ProductDetails");
        modelBuilder.Entity<Brand>().ToTable("Brands");

        modelBuilder.ApplyConfiguration(new EmployeeAverageSalaryConfiguration());
    }
}
