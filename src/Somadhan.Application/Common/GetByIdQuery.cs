using MediatR;

using Somadhan.Domain;

namespace Somadhan.Application.Common;

public class GetByIdQuery<TEntity> : IRequest<TEntity>
    where TEntity : EntityBase
{
    public string Id
    {
        get; set;
    } = default!;
}

