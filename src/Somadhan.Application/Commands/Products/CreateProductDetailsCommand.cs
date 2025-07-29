using MediatR;
using Somadhan.Application.Dtos;

namespace Somadhan.Application.Commands.Products
{
    public class CreateProductDetailsCommand : IRequest<ProductDetailsDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductCategoryId { get; set; }
        public string ShopId { get; set; }
    }
}
