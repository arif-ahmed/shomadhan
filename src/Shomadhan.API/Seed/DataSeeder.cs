using Microsoft.AspNetCore.Identity;

using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Identity;

namespace Shomadhan.API.Seed;
public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "SystemAdmin", "Admin", "Member" };
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        if (!db.Shops.Any())
        {
            db.Shops.Add(new Shop { Id = Guid.NewGuid().ToString(), Name = "Abdullah Traders", Email = "abdullah@gmail.com", PhoneNumber = "+8801676256810" });
            db.Shops.Add(new Shop { Id = Guid.NewGuid().ToString(), Name = "Atiq Halal Foods", Email = "atiq@gmail.com", PhoneNumber = "+8801676256820" });
            await db.SaveChangesAsync();
        }

        var sysAdminEmail = "sysadmin@demo.com";
        if (await userManager.FindByEmailAsync(sysAdminEmail) == null)
        {
            var user = new ApplicationUser
            {
                UserName = sysAdminEmail,
                Email = sysAdminEmail,
                EmailConfirmed = true,
                ShopId = Guid.Empty.ToString()
            };
            await userManager.CreateAsync(user, "SysAdminPass123!");
            await userManager.AddToRoleAsync(user, "SystemAdmin");
        }
    }
}
