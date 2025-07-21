using Somadhan.API.Middlewares;

namespace Somadhan.API.Extentions;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<TenantResolutionMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
