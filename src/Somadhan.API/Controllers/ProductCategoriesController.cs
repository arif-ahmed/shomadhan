using MediatR;
using Microsoft.AspNetCore.Mvc;
using Somadhan.Application.Commands.Products;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductCategoriesController : BaseController<ProductCategory, ProductCategoryDto>
    {
        public ProductCategoriesController(IMediator mediator) : base(mediator)
        {

        }
    }
}
