using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Somadhan.API.Constants;
using Somadhan.API.Filters;
using Somadhan.Application.Commands.Identity;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace Somadhan.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(InjectShopIdFilter))]
[Authorize(Roles = SystemRole.ShopManager)]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;
    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RoleDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [SwaggerOperation(
        Summary = "Get Role by ID",
        Description = "Retrieves a role by its unique identifier.",
        OperationId = "GetRoleById"
    )]
    [SwaggerResponse(200, "Role retrieved successfully", typeof(RoleDto))]
    [SwaggerResponse(404, "Role not found")]
    [SwaggerResponse(400, "Bad request")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> Get(string id)
    {
        var role = await _mediator.Send(new GetRoleByIdQuery(id));

        if (role == null)
        {
            return NotFound(new { Message = "Role not found." });
        }

        return Ok(role);
    }   

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all roles",
        Description = "Retrieves a list of all roles."
    )]
    [SwaggerResponse(200, "Roles retrieved successfully", typeof(IEnumerable<RoleDto>))]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerResponse(400, "Bad request")]    
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetRoles([FromQuery] string? searchText, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
    {
        string? shopId = User.FindFirst("ShopId")?.Value;
        var response = await _mediator.Send(new GetRolesQuery { SearchText = searchText, ShopId = shopId, PageNumber = pageNumber, PageSize = pageSize });
        return Ok(new
        {
            Data = response.Item1,
            TotalCount = response.Item2
        });
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new role",
        Description = "Creates a new role with the specified details.",
        OperationId = "CreateRole"
    )]
    [SwaggerResponse(200, "Role created successfully")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerResponse(400, "Bad request")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateRole(CreateRoleCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "Role created successfully" });
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update an existing role",
        Description = "Updates the details of an existing role.",
        OperationId = "UpdateRole"
    )]
    [SwaggerResponse(200, "Role updated successfully")]
    [SwaggerResponse(400, "Bad request")]
    [SwaggerResponse(404, "Role not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> UpdateRole(string id, UpdateRoleCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Role ID mismatch.");
        }
        await _mediator.Send(command);
        return Ok(new { Message = "Role updated successfully" });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a role",
        Description = "Deletes a role by its unique identifier.",
        OperationId = "DeleteRole"
    )]
    [SwaggerResponse(200, "Role deleted successfully")]
    [SwaggerResponse(404, "Role not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        await _mediator.Send(new DeleteRoleCommand { Id = id });
        return Ok(new { Message = "Role deleted successfully" });
    }
}
