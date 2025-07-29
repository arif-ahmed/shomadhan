using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Somadhan.API.Models;
using Somadhan.Application.Commands.Shops;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain.Modules.Order;

using Swashbuckle.AspNetCore.Annotations;

namespace Somadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class ShopsController : BaseController<Shop, ShopDto>
{
    private readonly ILogger<ShopsController> _logger;
    private readonly IMediator _mediator;
    public ShopsController(ILogger<ShopsController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetShopUsers()
    {
        _logger.LogInformation("Retrieving users for the shop");

        var shopId = User.FindFirst("ShopId")?.Value;

        var shopUsers = await _mediator.Send(new GetShopUsersQuery { ShopId = shopId });

        _logger.LogInformation("Retrieving users for the shop");

        _logger.LogInformation("Users retrieved successfully");

        return Ok(shopUsers);

    }
}
