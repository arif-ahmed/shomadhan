using System.Linq.Expressions;

using AutoMapper;

using MediatR;

using Somadhan.Application.Dtos;
using Somadhan.Domain;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Common.Handlers;
public class GetPagedQueryHandler<TDto, TEntity> : IRequestHandler<GetPagedQuery<TDto, TEntity>, PaginatedList<TDto>>
    where TEntity : class
{
    private readonly IEntityRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GetPagedQueryHandler(IEntityRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TDto>> Handle(GetPagedQuery<TDto, TEntity> request, CancellationToken cancellationToken)
    {
        Expression<Func<TEntity, bool>> predicate = u => true;

        var (brands, totalCount) = await _repository.FindAsync(predicate, request.Pagination.PageNumber, request.Pagination.PageSize);
        var dtos = _mapper.Map<List<TDto>>(brands);
        return new PaginatedList<TDto>(dtos, totalCount, request.Pagination.PageNumber, request.Pagination.PageSize);
    }
}
