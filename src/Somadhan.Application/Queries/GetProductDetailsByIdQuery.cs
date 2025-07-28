using MediatR;
using Somadhan.Application.Dtos;

namespace Somadhan.Application.Queries
{
    public class GetProductDetailsByIdQuery : IRequest<ProductDetailsDto>
    {
        public string Id { get; set; }
    }
}
