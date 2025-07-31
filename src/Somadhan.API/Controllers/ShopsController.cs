using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Somadhan.API.Models;
using Somadhan.Application.Commands.Shops;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Order;
using Somadhan.Persistence.Mongo;

using Swashbuckle.AspNetCore.Annotations;

namespace Somadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
// [Authorize]
public class ShopsController : BaseController<Shop, ShopDto>
{
    private readonly ILogger<ShopsController> _logger;
    private readonly IMediator _mediator;
    private readonly MongoShopRepository _repository;
    public ShopsController(ILogger<ShopsController> logger, IMediator mediator, MongoShopRepository repository) : base(mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

    [HttpGet("test-shops")]
    public async Task<IActionResult> TestShops()
    {
        var shops = await _repository.GetAllAsync();

        return Ok(shops);
    }
}
