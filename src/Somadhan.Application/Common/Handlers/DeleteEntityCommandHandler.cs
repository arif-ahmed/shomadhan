using MediatR;

using Somadhan.Domain;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Common.Handlers;
public class DeleteEntityCommandHandler<TEntity> : IRequestHandler<DeleteEntityCommand<TEntity>>
    where TEntity : EntityBase
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteEntityCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task Handle(DeleteEntityCommand<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.GetRepository<TEntity>().GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {request.Id} not found.");

        await _unitOfWork.GetRepository<TEntity>().DeleteAsync(entity.Id);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}

