using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;
using AutoMapper;

namespace Somadhan.Application.Queries;

public class GetProductCategoriesQuery : IRequest<IEnumerable<ProductCategoryDto>>
{
}

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, IEnumerable<ProductCategoryDto>>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetProductCategoriesQueryHandler(IProductCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductCategoryDto>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductCategoryDto>>(categories);
    }
}
