using MediatR;
using Somadhan.Application.Dtos;

namespace Somadhan.Application.Queries;
public class GetBrandByIdQuery : IRequest<BrandDto>
{
    public string Id { get; set; } = default!;
}
