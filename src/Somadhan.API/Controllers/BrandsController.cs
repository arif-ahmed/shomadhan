using MediatR;
using Microsoft.AspNetCore.Mvc;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain;

namespace Somadhan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetBrandByIdQuery { Id = id });

        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<BrandDto>>> Get([FromQuery] GetBrandsQuery query)
    {
        var brands = await _mediator.Send(query);
        return Ok(brands);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
    {
        if (command == null)
            return BadRequest("Invalid brand data.");
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateBrandCommand command)
    {
        if (command == null || command.Id != id)
            return BadRequest("Invalid brand data.");
        
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var isSucceed = await _mediator.Send(new DeleteBrandCommand { Id = id });
        if (isSucceed)
            return NotFound();
        
        return NoContent();
    }
}
