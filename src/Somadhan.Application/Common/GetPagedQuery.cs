using MediatR;

using Somadhan.Domain;

namespace Somadhan.Application.Common;
public class GetPagedQuery<TDto, TEntity> : IRequest<PaginatedList<TDto>>
{
    public PaginationQuery Pagination { get; set; } = new();
}
