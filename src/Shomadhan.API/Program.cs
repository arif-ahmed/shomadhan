using Microsoft.AspNetCore.Identity;
using Shomadhan.API.Extentions;
using Shomadhan.API.Seed;
using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// --- Service registrations via extensions ---
builder.Services
    .AddAppDbContext(builder.Configuration)
    .AddAppIdentity()
    .AddRepositories()
    .AddMediatorAndValidators()
    .AddJwtAuthentication(builder.Configuration)
    .AddApiServices()
    .AddSwaggerDocumentation();

var app = builder.Build();

// --- Data Seeding ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DataSeeder.SeedAsync(db, userManager, roleManager);
}

// --- Middleware pipeline ---
app.UseAppMiddlewares();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapGet("/", () => "Inventory Service API is running!").ExcludeFromDescription();

app.Run();
