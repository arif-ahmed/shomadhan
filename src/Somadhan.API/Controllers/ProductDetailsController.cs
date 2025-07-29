using MediatR;
using Microsoft.AspNetCore.Mvc;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain.Modules.Product;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Somadhan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductDetailsController : BaseController<ProductDetails, ProductDetailsDto>
    {
        public ProductDetailsController(IMediator mediator) : base(mediator)
        {

        }
    }
}
