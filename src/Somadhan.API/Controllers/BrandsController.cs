using MediatR;
using Microsoft.AspNetCore.Mvc;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : BaseController<Brand, BrandDto>
{
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }
}
