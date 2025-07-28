using MediatR;

namespace Somadhan.Application.Commands.Products
{
    public class DeleteProductDetailsCommand : IRequest
    {
        public string Id { get; set; }
    }
}
