using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shomadhan.API.Filters;
using Shomadhan.Application.Commands.Identity;
using Shomadhan.Application.Queries;

namespace Shomadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(InjectShopIdFilter))]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;
    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _mediator.Send(new GetRolesQuery());
        return Ok(roles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "Role created successfully" });
    }
}
