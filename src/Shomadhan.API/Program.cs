using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shomadhan.API.Filters;
using Shomadhan.API.Middlewares;
using Shomadhan.API.Validators;
using Shomadhan.Application.Commands.Products;
using Shomadhan.Application.Common.Behaviors;
using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure;
using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Identity;
using Shomadhan.Infrastructure.Repositories;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AuthDemoDb"));


// Register generic repository
builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
// Register custom repositories
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IRoleRepository, IdentityRoleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCategoryCommand).Assembly);
});

//builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCategoryCommand>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Add JWT Authentication
var jwtKey = "MyUltraSecureSecretKey123!"; // move to appsettings in production
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<InjectShopIdFilter>();

builder.Services.AddControllers(options => 
{
    // options.Filters.Add<InjectShopIdFilter>();
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateShopRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authentication using username and password"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });


    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
// Seed admin user and a test shop (see Step 6)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData(db, userManager, roleManager);
}

app.UseMiddleware<TenantResolutionMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Inventory Service API is running!").ExcludeFromDescription();
app.Run();

async Task SeedData(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string[] roles = { "SystemAdmin", "Admin", "Member" };
    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    if (!db.Shops.Any())
    {
        db.Shops.Add(new Shop { Id = Guid.CreateVersion7().ToString(), Name = "Abdullah Traders", Email = "abdullah@gmail.com", PhoneNumber = "+8801676256810"  });
        db.Shops.Add(new Shop { Id = Guid.CreateVersion7().ToString(), Name = "Atiq Halal Foods", Email = "atiq@gmail.com", PhoneNumber = "+8801676256820" });
        await db.SaveChangesAsync();
    }

    // Seed System Admin
    var sysAdminEmail = "sysadmin@demo.com";
    if (await userManager.FindByEmailAsync(sysAdminEmail) == null)
    {
        var user = new ApplicationUser
        {
            UserName = sysAdminEmail,
            Email = sysAdminEmail,
            EmailConfirmed = true,
            ShopId = Guid.Empty.ToString() // System admin, not tied to shop
        };
        await userManager.CreateAsync(user, "SysAdminPass123!");
        await userManager.AddToRoleAsync(user, "SystemAdmin");
    }

    //// Seed Shop Admin
    //var shopAdminEmail = "shopadmin@demo.com";
    //if (await userManager.FindByEmailAsync(shopAdminEmail) == null)
    //{
    //    var user = new ApplicationUser
    //    {
    //        UserName = shopAdminEmail,
    //        Email = shopAdminEmail,
    //        EmailConfirmed = true,
    //        ShopId = 1
    //    };
    //    await userManager.CreateAsync(user, "ShopAdminPass123!");
    //    await userManager.AddToRoleAsync(user, "Admin");
    //}
}
