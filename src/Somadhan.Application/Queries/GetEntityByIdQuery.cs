using MediatR;

using Somadhan.Domain;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Queries;

public class GetEntityByIdQuery<T> : IRequest<T> where T : EntityBase
{
    public string Id { get; set; } = default!;
    public GetEntityByIdQuery(string id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}

public class GetEntityByIdHandler<T> : IRequestHandler<GetEntityByIdQuery<T>, T>
    where T : EntityBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IEntityRepository<T> _entityRepository;

    public GetEntityByIdHandler(IMediator mediator, IUnitOfWork unitOfWork, IEntityRepository<T> entityRepository)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
    }

    public async Task<T> Handle(GetEntityByIdQuery<T> request, CancellationToken cancellationToken)
    {
        var entities = await _entityRepository.GetByIdAsync(request.Id, cancellationToken);
        return entities ?? throw new KeyNotFoundException($"Entity with ID {request.Id} not found.");
    }
}
