using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Somadhan.Application.Queries;
using Somadhan.Domain;

namespace Somadhan.API.Controllers;

public class BaseController<TEntity, TDto> : ControllerBase where TEntity : EntityBase
{
    protected ILogger<BaseController<TEntity, TDto>> _logger;
    protected readonly IMediator _mediator;
    public BaseController(ILogger<BaseController<TEntity, TDto>> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [AllowAnonymous] // Default; can override in derived controller
    public virtual async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetEntityByIdQuery<TEntity>(id));
        return result == null ? NotFound() : Ok(result);
    }

    //[HttpPost]
    //[Authorize(Roles = "Admin")] // Example; can override
    //public virtual async Task<IActionResult> Create([FromBody] TDto dto)
    //{
    //    var result = await _mediator.Send(new CreateEntityCommand<TDto>(dto));
    //    return CreatedAtAction(nameof(GetById), new
    //    {
    //        id = result.Id
    //    }, result);
    //}
}

