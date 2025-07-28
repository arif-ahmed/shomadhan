using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Somadhan.API.Extentions;
public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Somadhan API", Version = "v1" });

            options.DocumentFilter<TagOrderDocumentFilter>();

            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            //foreach (var description in provider.ApiVersionDescriptions)
            //{
            //    options.SwaggerDoc(description.GroupName,
            //        new OpenApiInfo
            //        {
            //            Title = $"Somadhan API {description.ApiVersion}",
            //            Version = description.ApiVersion.ToString()
            //        });
            //}

            options.EnableAnnotations();

            // Basic Auth
            options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authentication using username and password"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
                    },
                    Array.Empty<string>()
                }
            });
            // JWT Bearer Auth
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}

public class TagOrderDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var orderedTags = new[]
        {
            "Shops",
            "Users",
            "Roles",
            "ProductCategories",
            "ProductDetails"
        };

        swaggerDoc.Tags = orderedTags
            .Select(t => new OpenApiTag { Name = t })
            .ToList();
    }
}

