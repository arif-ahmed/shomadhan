using MediatR;

using Somadhan.Domain;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Common;

public class GetByIdQueryHandler<TEntity> : IRequestHandler<GetByIdQuery<TEntity>, TEntity>
    where TEntity : EntityBase
{
    private readonly IEntityRepository<TEntity> _repository;

    public GetByIdQueryHandler(IEntityRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity> Handle(GetByIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.IsDeleted)
        {
            throw new KeyNotFoundException($"Entity with ID {request.Id} not found or is deleted.");
        }

        return entity;
    }
}
