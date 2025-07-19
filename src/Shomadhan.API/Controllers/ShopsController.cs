using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shomadhan.Application.Commands.Shops;
using Shomadhan.Application.Queries;

namespace Shomadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopsController : ControllerBase
{
    private readonly ILogger<ShopsController> _logger;
    private readonly IMediator _mediator;
    public ShopsController(ILogger<ShopsController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetShopsAsync([FromQuery] string? searchText, int offset = 1, int pageSize = 100)
    {
        var shops = await _mediator.Send(new GetShopsQuery { SearchText = searchText, Offset = offset, PageSize = pageSize });
        return Ok(new { Data = shops.Item1, TotalCount = shops.Item2 });
    }

    [HttpPost("Request")]
    public async Task<IActionResult> RequestShopRegistration(CreateShopCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "Shop created successfully" });
    }

    [HttpPatch("{shopId}/approval")]
    public async Task<IActionResult> ChangeShopApprovalStatus(string shopId, [FromBody] bool isApproved)
    {
        await _mediator.Send(new ApproveShopCommand { ShopId = shopId, IsApproved = isApproved });
        var message = isApproved ? "approved" : "rejected";
        return Ok(new { Message = $"Shop with ID {shopId} {message} successfully" });
    }

    [HttpGet("{shopId}")]
    public async Task<IActionResult> GetShopByIdAsync(string shopId)
    {
        // Placeholder for getting a shop by ID
        await Task.Delay(1000); // Simulate async operation
        return Ok(new { Message = $"Details of shop with ID {shopId}" });
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchShopsAsync([FromQuery] string query)
    {
        // Placeholder for searching shops
        await Task.Delay(1000); // Simulate async operation
        return Ok(new { Message = $"Search results for query: {query}" });
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetShopCategoriesAsync()
    {
        // Placeholder for getting shop categories
        await Task.Delay(1000); // Simulate async operation
        return Ok(new { Message = "List of shop categories" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShop(string id, [FromBody] UpdateShopCommand command)
    {
        _logger.LogInformation("Updating shop with ID: {ShopId}", id);

        if (string.IsNullOrEmpty(id))
        {
            _logger.LogError("Shop ID is null or empty.");
            return BadRequest("Shop ID cannot be null or empty.");
        }

        if (command == null)
        {
            _logger.LogError("Customer data is null for ID: {CustomerId}", id);
            return BadRequest("Customer data cannot be null.");
        }

        await _mediator.Send(command);
        
        return Ok(new { Message = $"Shop with ID {id} updated successfully" });
    }
}
