using AutoMapper;

using MediatR;

using Somadhan.Domain;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Common.Handlers;
public class CreateHandler<TDto, TEntity> : IRequestHandler<CreateCommand<TDto, TEntity>>
    where TEntity : EntityBase, new()
{
    private IUnitOfWork _unitOfWork;
    private IEntityRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public CreateHandler(IUnitOfWork unitOfWork, IEntityRepository<TEntity> repository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateCommand<TDto, TEntity> request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(request.Dto);
        // await _repository.AddAsync(entity, cancellationToken);

        _repository = _unitOfWork.GetRepository<TEntity>();
        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        //return _mapper.Map<TDto>(result);
    }
}

