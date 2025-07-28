using System.Text;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Somadhan.API.Filters;
using Somadhan.API.Validators;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Common;
using Somadhan.Application.Common.Behaviors;
using Somadhan.Application.Common.Handlers;
using Somadhan.Application.Dtos;
using Somadhan.Application.Validators;
using Somadhan.Domain;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;
using Somadhan.Infrastructure;
using Somadhan.Infrastructure.Data;
using Somadhan.Infrastructure.Identity;
using Somadhan.Infrastructure.Repositories;

namespace Somadhan.API.Extentions;
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
        services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
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
            // cfg.RegisterServicesFromAssembly(typeof(TestCommandHandler<>).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateProductCategoryCommand).Assembly);
        });
        services.AddAutoMapper(typeof(CreateProductCategoryCommand).Assembly);

        // services.AddTransient(typeof(IRequestHandler<,>), typeof(CreateHandler<,>));
        // services.AddTransient(typeof(IRequestHandler<,>), typeof(TestCommandHandler<>));

        services.AddTransient<IRequestHandler<GetByIdQuery<Shop>, Shop>, GetByIdQueryHandler<Shop>>();
        services.AddTransient<IRequestHandler<GetPagedQuery<ShopDto, Shop>, PaginatedList<ShopDto>>, GetPagedQueryHandler<ShopDto, Shop>>();
        services.AddTransient<IRequestHandler<CreateCommand<ShopDto, Shop>>, CreateHandler<ShopDto, Shop>>();
        services.AddTransient<IRequestHandler<UpdateEntityCommand<ShopDto>, ShopDto>, UpdateEntityCommandHandler<Shop, ShopDto>>();
        services.AddTransient<IRequestHandler<DeleteEntityCommand<Shop>>, DeleteEntityCommandHandler<Shop>>();

        services.AddTransient<IRequestHandler<GetByIdQuery<ProductCategory>, ProductCategory>, GetByIdQueryHandler<ProductCategory>>();
        services.AddTransient<IRequestHandler<GetPagedQuery<ProductCategoryDto, ProductCategory>, PaginatedList<ProductCategoryDto>>, GetPagedQueryHandler<ProductCategoryDto, ProductCategory>>();
        services.AddTransient<IRequestHandler<CreateCommand<ProductCategoryDto, ProductCategory>>, CreateHandler<ProductCategoryDto, ProductCategory>>();
        services.AddTransient<IRequestHandler<UpdateEntityCommand<ProductCategoryDto>, ProductCategoryDto>, UpdateEntityCommandHandler<ProductCategory, ProductCategoryDto>>();
        services.AddTransient<IRequestHandler<DeleteEntityCommand<ProductCategory>>, DeleteEntityCommandHandler<ProductCategory>>();

        services.AddTransient<IRequestHandler<GetByIdQuery<ProductDetails>, ProductDetails>, GetByIdQueryHandler<ProductDetails>>();
        services.AddTransient<IRequestHandler<GetPagedQuery<ProductDetailsDto, ProductDetails>, PaginatedList<ProductDetailsDto>>, GetPagedQueryHandler<ProductDetailsDto, ProductDetails>>();
        services.AddTransient<IRequestHandler<CreateCommand<ProductDetailsDto, ProductDetails>>, CreateHandler<ProductDetailsDto, ProductDetails>>();
        services.AddTransient<IRequestHandler<UpdateEntityCommand<ProductDetailsDto>, ProductDetailsDto>, UpdateEntityCommandHandler<ProductDetails, ProductDetailsDto>>();
        services.AddTransient<IRequestHandler<DeleteEntityCommand<ProductDetails>>, DeleteEntityCommandHandler<ProductDetails>>();

        services.AddTransient<IRequestHandler<GetByIdQuery<Brand>, Brand>, GetByIdQueryHandler<Brand>>();
        services.AddTransient<IRequestHandler<GetPagedQuery<BrandDto, Brand>, PaginatedList<BrandDto>>, GetPagedQueryHandler<BrandDto, Brand>>();
        services.AddTransient<IRequestHandler<CreateCommand<BrandDto, Brand>>, CreateHandler<BrandDto, Brand>>();
        services.AddTransient<IRequestHandler<UpdateEntityCommand<BrandDto>, BrandDto>, UpdateEntityCommandHandler<Brand, BrandDto>>();
        services.AddTransient<IRequestHandler<DeleteEntityCommand<Brand>>, DeleteEntityCommandHandler<Brand>>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<CreateShopRequestValidator>();
        // services.AddValidatorsFromAssemblyContaining<CreateBrandValidator>();

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
