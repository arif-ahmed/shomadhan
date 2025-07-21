using System.Security.Claims;

namespace Somadhan.API.Middlewares;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var host = context.Request.Host.Host; // e.g., tenant1.myapp.com
        var tenant = host.Split('.')[0]; // "tenant1" if subdomain is first part

        // Store tenant info for the rest of the request, e.g. in Items
        context.Items["Tenant"] = tenant;


        var user = context.User; // ClaimsPrincipal
        if (user.Identity?.IsAuthenticated == true)
        {
            var shopId = user.FindFirst("ShopId")?.Value;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // ...any other claim you wish
        }

        await _next(context);
    }
}


