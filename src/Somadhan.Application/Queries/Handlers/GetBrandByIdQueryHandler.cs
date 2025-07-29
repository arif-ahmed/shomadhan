using AutoMapper;
using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Application.Queries;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Queries.Handlers;
public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id);
        return _mapper.Map<BrandDto>(brand);
    }
}
