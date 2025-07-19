using Microsoft.AspNetCore.Identity;

using Shomadhan.Domain.Modules.Product;
using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Identity;

namespace Shomadhan.API.Seed;
public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "SystemAdmin", "Admin", "ShopOwner", "SalesRepresentative" };
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        // Seed Shops
        if (!db.Shops.Any())
        {
            var shops = new List<Shop>
        {
            new Shop { Id = Guid.NewGuid().ToString(), Name = "Abdullah Traders", Email = "abdullah@gmail.com", PhoneNumber = "+8801676256810" },
            new Shop { Id = Guid.NewGuid().ToString(), Name = "Wafilife", Email = "wafilife@gmail.com", PhoneNumber = "+8801676256821" },
            new Shop { Id = Guid.NewGuid().ToString(), Name = "Atik Fragnance", Email = "atikfrag@gmail.com", PhoneNumber = "+8801676256822" }
        };
            db.Shops.AddRange(shops);
            await db.SaveChangesAsync();
        }

        var shopsDict = db.Shops.ToDictionary(s => s.Name, s => s);

        // Seed Product Categories and Products for Each Shop
        if (!db.ProductCategories.Any())
        {
            // -------- Abdullah Traders --------
            var shop1 = shopsDict["Abdullah Traders"];
            var riceCat1 = new ProductCategory { Id = Guid.NewGuid().ToString(), Name = "Rice", Description = "Variety of rices", ShopId = shop1.Id, Shop = shop1 };
            var spicesCat1 = new ProductCategory { Id = Guid.NewGuid().ToString(), Name = "Spices", Description = "Aromatic spices", ShopId = shop1.Id, Shop = shop1 };

            db.ProductCategories.AddRange(riceCat1, spicesCat1);

            db.ProductDetails.AddRange(
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Basmati Rice (5kg)", Description = "Premium Basmati Rice", Price = 9.99M, StockQuantity = 100, ProductCategoryId = riceCat1.Id, ProductCategory = riceCat1, ShopId = shop1.Id, Shop = shop1 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Brown Rice (5kg)", Description = "Healthy Brown Rice", Price = 8.50M, StockQuantity = 80, ProductCategoryId = riceCat1.Id, ProductCategory = riceCat1, ShopId = shop1.Id, Shop = shop1 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Turmeric Powder (200g)", Description = "Pure Turmeric", Price = 2.20M, StockQuantity = 50, ProductCategoryId = spicesCat1.Id, ProductCategory = spicesCat1, ShopId = shop1.Id, Shop = shop1 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Chilli Powder (200g)", Description = "Hot Chilli Powder", Price = 2.50M, StockQuantity = 60, ProductCategoryId = spicesCat1.Id, ProductCategory = spicesCat1, ShopId = shop1.Id, Shop = shop1 }
            );

            // -------- Wafilife --------
            var shop2 = shopsDict["Wafilife"];
            var booksCat2 = new ProductCategory { Id = Guid.NewGuid().ToString(), Name = "Islamic Books", Description = "Islamic Books", ShopId = shop2.Id, Shop = shop2 };
            var stationeryCat2 = new ProductCategory { Id = Guid.NewGuid().ToString(), Name = "Stationery", Description = "Office and study supplies", ShopId = shop2.Id, Shop = shop2 };

            db.ProductCategories.AddRange(booksCat2, stationeryCat2);

            db.ProductDetails.AddRange(
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Qur’an with Translation", Description = "Translation and commentary", Price = 5.99M, StockQuantity = 40, ProductCategoryId = booksCat2.Id, ProductCategory = booksCat2, ShopId = shop2.Id, Shop = shop2 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Stories of the Prophets", Description = "Stories for children and adults", Price = 6.99M, StockQuantity = 35, ProductCategoryId = booksCat2.Id, ProductCategory = booksCat2, ShopId = shop2.Id, Shop = shop2 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Notebook (A5, 100 pages)", Description = "A5 notebook", Price = 1.20M, StockQuantity = 100, ProductCategoryId = stationeryCat2.Id, ProductCategory = stationeryCat2, ShopId = shop2.Id, Shop = shop2 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Ball Pen (Blue)", Description = "Blue ink ballpoint", Price = 0.30M, StockQuantity = 300, ProductCategoryId = stationeryCat2.Id, ProductCategory = stationeryCat2, ShopId = shop2.Id, Shop = shop2 }
            );

            // -------- Atik Fragnance --------
            var shop3 = shopsDict["Atik Fragnance"];
            var attarCat3 = new ProductCategory { Id = Guid.NewGuid().ToString(), Name = "Attar", Description = "Natural fragrance oils", ShopId = shop3.Id, Shop = shop3 };
            var perfumeCat3 = new ProductCategory { Id = Guid.NewGuid().ToString(), Name = "Perfume", Description = "Fragrances", ShopId = shop3.Id, Shop = shop3 };

            db.ProductCategories.AddRange(attarCat3, perfumeCat3);

            db.ProductDetails.AddRange(
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Oudh Attar (10ml)", Description = "Traditional Oudh attar", Price = 7.99M, StockQuantity = 20, ProductCategoryId = attarCat3.Id, ProductCategory = attarCat3, ShopId = shop3.Id, Shop = shop3 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Musk Attar (10ml)", Description = "Classic musk attar", Price = 8.99M, StockQuantity = 15, ProductCategoryId = attarCat3.Id, ProductCategory = attarCat3, ShopId = shop3.Id, Shop = shop3 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Oudh Perfume (50ml)", Description = "Long lasting Oudh perfume", Price = 25.00M, StockQuantity = 10, ProductCategoryId = perfumeCat3.Id, ProductCategory = perfumeCat3, ShopId = shop3.Id, Shop = shop3 },
                new ProductDetails { Id = Guid.NewGuid().ToString(), Name = "Musk Perfume (50ml)", Description = "Long lasting Musk perfume", Price = 22.00M, StockQuantity = 12, ProductCategoryId = perfumeCat3.Id, ProductCategory = perfumeCat3, ShopId = shop3.Id, Shop = shop3 }
            );

            await db.SaveChangesAsync();
        }

        // Seed Users (as in previous response)
        var usersToCreate = new[]
        {
            // Abdullah Traders
            new { Email = "abdullah.owner@gmail.com", Role = "ShopOwner", Shop = "Abdullah Traders", Password = "AbdullahOwner123!" },
            new { Email = "abdullah.admin@gmail.com", Role = "Admin", Shop = "Abdullah Traders", Password = "AbdullahAdmin123!" },
            new { Email = "abdullah.rep1@gmail.com", Role = "SalesRepresentative", Shop = "Abdullah Traders", Password = "AbdullahRep123!" },
            new { Email = "abdullah.rep2@gmail.com", Role = "SalesRepresentative", Shop = "Abdullah Traders", Password = "AbdullahRep123!" },

            // Wafilife
            new { Email = "wafilife.owner@gmail.com", Role = "ShopOwner", Shop = "Wafilife", Password = "WafilifeOwner123!" },
            new { Email = "wafilife.admin@gmail.com", Role = "Admin", Shop = "Wafilife", Password = "WafilifeAdmin123!" },
            new { Email = "wafilife.rep1@gmail.com", Role = "SalesRepresentative", Shop = "Wafilife", Password = "WafilifeRep123!" },
            new { Email = "wafilife.rep2@gmail.com", Role = "SalesRepresentative", Shop = "Wafilife", Password = "WafilifeRep123!" },

            // Atik Fragnance
            new { Email = "atikfrag.owner@gmail.com", Role = "ShopOwner", Shop = "Atik Fragnance", Password = "AtikOwner123!" },
            new { Email = "atikfrag.admin@gmail.com", Role = "Admin", Shop = "Atik Fragnance", Password = "AtikAdmin123!" },
            new { Email = "atikfrag.rep1@gmail.com", Role = "SalesRepresentative", Shop = "Atik Fragnance", Password = "AtikRep123!" },
            new { Email = "atikfrag.rep2@gmail.com", Role = "SalesRepresentative", Shop = "Atik Fragnance", Password = "AtikRep123!" },
        };

        foreach (var u in usersToCreate)
        {
            if (await userManager.FindByEmailAsync(u.Email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = u.Email,
                    Email = u.Email,
                    EmailConfirmed = true,
                    ShopId = shopsDict[u.Shop].Id
                };
                var result = await userManager.CreateAsync(user, u.Password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, u.Role);
                // You may log errors here for failed results
            }
        }

        // System Admin
        var sysAdminEmail = "sysadmin@demo.com";
        if (await userManager.FindByEmailAsync(sysAdminEmail) == null)
        {
            var user = new ApplicationUser
            {
                UserName = sysAdminEmail,
                Email = sysAdminEmail,
                EmailConfirmed = true,
                ShopId = null // No shop for sysadmin
            };
            await userManager.CreateAsync(user, "SysAdminPass123!");
            await userManager.AddToRoleAsync(user, "SystemAdmin");
        }
    }
}
