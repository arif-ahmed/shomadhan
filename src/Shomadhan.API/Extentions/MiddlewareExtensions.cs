using Shomadhan.API.Middlewares;

namespace Shomadhan.API.Extentions;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<TenantResolutionMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
