using Microsoft.AspNetCore.Mvc.Filters;
using Somadhan.Application.Commands.Identity;

namespace Somadhan.API.Filters;
public class InjectShopIdFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.Values
            .FirstOrDefault(arg => arg is CreateRoleCommand) is CreateRoleCommand command)
        {
            if (context.HttpContext.Items["ShopId"] is string shopId)
            {
                command.ShopId = shopId;
            }

            command.ShopId = "12345";
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}

