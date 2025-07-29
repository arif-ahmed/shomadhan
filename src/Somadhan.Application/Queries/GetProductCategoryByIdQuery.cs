using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;
using AutoMapper;

namespace Somadhan.Application.Queries;

public class GetProductCategoryByIdQuery : IRequest<ProductCategoryDto>
{
    public string Id { get; set; }

    public GetProductCategoryByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, ProductCategoryDto>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetProductCategoryByIdQueryHandler(IProductCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductCategoryDto> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id);
        return _mapper.Map<ProductCategoryDto>(category);
    }
}