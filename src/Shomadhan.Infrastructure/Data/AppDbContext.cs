using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Domain.Modules.Product;
using Shomadhan.Infrastructure.Identity;

namespace Shomadhan.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Shop> Shops { get; set; }
    public DbSet<Role> ApplicationRoles { get; set; }
    public DbSet<User> ApplicationUsers { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductDetails> ProductDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Additional model configurations can be added here
        modelBuilder.Entity<Shop>().ToTable("Shops");
    }
}

