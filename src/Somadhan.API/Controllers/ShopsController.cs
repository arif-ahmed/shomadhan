using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Somadhan.API.Models;
using Somadhan.Application.Commands.Shops;
using Somadhan.Application.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace Somadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ShopsController : ControllerBase
{
    private readonly ILogger<ShopsController> _logger;
    private readonly IMediator _mediator;
    public ShopsController(ILogger<ShopsController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{shopId}")]
    [SwaggerOperation(Summary = "Get shop by ID", Description = "Retrieves the details of a shop using its unique identifier.")]
    [SwaggerResponse(200, "Shop details retrieved successfully", typeof(object))]
    [SwaggerResponse(404, "Shop not found")]
    [SwaggerResponse(500, "Internal server error")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    // <summary>
    /// Gets the details of a shop by its ID.
    // </summary>
    public async Task<IActionResult> GetShopByIdAsync(string shopId)
    {
        // Placeholder for getting a shop by ID
        await Task.Delay(1000); // Simulate async operation
        return Ok(new
        {
            Message = $"Details of shop with ID {shopId}"
        });
    }

    [HttpGet]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
    [SwaggerOperation(Summary = "Get all shops", Description = "Retrieves a paginated list of all shops, optionally filtered by search text.")]
    [SwaggerResponse(200, "List of shops retrieved successfully", typeof(object))]
    [SwaggerResponse(400, "Bad request", typeof(string))]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> GetShopsAsync([FromQuery] string? searchText, int pageNumber = 1, int pageSize = 100)
    {
        var shops = await _mediator.Send(new GetShopsQuery { SearchText = searchText, PageNumber = pageNumber, PageSize = pageSize });
        return Ok(new { Data = shops.Item1, TotalCount = shops.Item2 });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new shop", Description = "Creates a new shop with the provided details.")]
    [SwaggerResponse(201, "Shop created successfully", typeof(CreateShopResponse))]
    [SwaggerResponse(400, "Bad request", typeof(string))]
    [SwaggerResponse(500, "Internal server error")]
    [ProducesResponseType(typeof(CreateShopResponse), 201)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(500)]
    /// <summary>
    /// Creates a new shop.
    /// </summary> 
    public async Task<IActionResult> Post([FromBody] CreateShopCommand command)
    {
        _logger.LogInformation("Creating a new shop with command: {@Command}", command);

        var newShopId = await _mediator.Send(command);

        if (string.IsNullOrEmpty(newShopId))
        {
            _logger.LogError("Failed to create shop. Command: {@Command}", command);
            return BadRequest("Failed to create shop.");
        }

        _logger.LogInformation("Shop created successfully with ID: {ShopId}", newShopId);

        // Build absolute URI
        var location = Url.Action(nameof(GetShopByIdAsync),
                                  controller: "Shops",
                                  values: new
                                  {
                                      shopId = newShopId
                                  },
                                  protocol: Request.Scheme);

        return Created(location, new CreateShopResponse
        {
            Message = "Shop created successfully",
            ShopId = newShopId
        });

        //return CreatedAtRoute(
        //    nameof(GetShopByIdAsync),
        //    new
        //    {
        //        shopId = newShopId
        //    }
        //);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateShopCommand command)
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

        return Ok(new
        {
            Message = $"Shop with ID {id} updated successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShop(string id)
    {
        _logger.LogInformation("Deleting shop with ID: {ShopId}", id);
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogError("Shop ID is null or empty.");
            return BadRequest("Shop ID cannot be null or empty.");
        }
        await _mediator.Send(new DeleteShopCommand { Id = id });

        _logger.LogInformation("Shop with ID: {ShopId} deleted successfully", id);

        return Ok(new
        {
            Message = $"Shop with ID {id} deleted successfully"
        });
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
