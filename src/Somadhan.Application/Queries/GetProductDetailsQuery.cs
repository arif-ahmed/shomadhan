using MediatR;
using Somadhan.Application.Dtos;
using System.Collections.Generic;

namespace Somadhan.Application.Queries
{
    public class GetProductDetailsQuery : IRequest<IEnumerable<ProductDetailsDto>>
    {
    }
}
