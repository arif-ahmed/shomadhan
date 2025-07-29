using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Somadhan.Application.Queries;

namespace Somadhan.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
// [Authorize]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;
    public UsersController(ILogger<UsersController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? searchText, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
    {
        string? shopId = User.FindFirst("ShopId")?.Value;

        var response = await _mediator.Send(new GetUsersQuery { SearchText = searchText, ShopId = shopId, PageNumber = pageNumber, PageSize = pageSize });

        return Ok(new
        {
            Data = response.Item1,
            TotalCount = response.Item2
        });
    }
}
