using System.Linq.Expressions;

using AutoMapper;

using MediatR;

using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain;
using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Business;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Queries.Handlers;
public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, PaginatedList<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Brand, bool>> predicate = u => true;

        var (brands, totalCount) = await _brandRepository.FindAsync(predicate, request.PageNumber, request.PageSize);
        var brandDtos = _mapper.Map<List<BrandDto>>(brands);
        return new PaginatedList<BrandDto>(brandDtos, totalCount, request.PageNumber, request.PageSize);
    }
}
