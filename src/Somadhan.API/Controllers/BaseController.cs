using MediatR;

using Microsoft.AspNetCore.Mvc;

using Somadhan.Application.Common;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.API.Controllers;

public abstract class BaseController<TEntity, TDto> : ControllerBase where TEntity : EntityBase
{
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetByIdQuery<TEntity> { Id = id });
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<TDto>>> Get([FromQuery] GetPagedQuery<TDto, TEntity> query)
    {
        var brands = await _mediator.Send(query);
        return Ok(brands);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TDto dto)
    {        
        await _mediator.Send(new CreateCommand<TDto, TEntity> { Dto = dto });
        return Ok();
        //return CreatedAtAction(
        //    nameof(GetById),
        //    new
        //    {
        //        id = (result as dynamic).Id
        //    },  
        //    result
        //);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TDto dto)
    {
        var result = await _mediator.Send(new UpdateEntityCommand<TDto>(id, dto));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteBrand(string id)
    {
        await _mediator.Send(new DeleteEntityCommand<TEntity>(Id: id));
        return NoContent();
    }

}
