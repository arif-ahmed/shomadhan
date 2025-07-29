using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain;

namespace Somadhan.Application.Queries;
public class GetBrandsQuery : IRequest<PaginatedList<BrandDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
