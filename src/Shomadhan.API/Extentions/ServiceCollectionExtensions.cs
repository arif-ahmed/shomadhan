using System.Text;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Shomadhan.API.Filters;
using Shomadhan.API.Validators;
using Shomadhan.Application.Commands.Products;
using Shomadhan.Application.Common.Behaviors;
using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure;
using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Identity;
using Shomadhan.Infrastructure.Repositories;

namespace Shomadhan.API.Extentions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // UseInMemoryDatabase is just for dev/testing. Replace with real provider for production.
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("AuthDemoDb"));
        return services;
    }

    public static IServiceCollection AddAppIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<IRoleRepository, IdentityRoleRepository>();
        services.AddScoped<IUserRepository, IdentityUserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddMediatorAndValidators(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateProductCategoryCommand).Assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<CreateShopRequestValidator>();
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        })
        .AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<InjectShopIdFilter>();
        services.AddControllers();
        return services;
    }
}
