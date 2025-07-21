using MediatR;
using Microsoft.AspNetCore.Mvc;
using Somadhan.Application.Commands.Products;

namespace Somadhan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductCategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            await Task.Delay(3000);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Task.Delay(3000);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCategoryCommand request)
        {
            var productCategoryId = await _mediator.Send(request);
            return Ok(productCategoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CreateProductCategoryCommand request)
        {
            if (id != request.Name) // Assuming 'Name' is the unique identifier for the category
            {
                return BadRequest("Category ID mismatch.");
            }
            // Logic to update the product category would go here
            await Task.Delay(3000);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Logic to delete the product category would go here
            await Task.Delay(3000);
            return NoContent();
        }
    }
}
