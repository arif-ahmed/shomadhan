using AutoMapper;

using MediatR;

using Somadhan.Domain;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Common.Handlers;
public class UpdateEntityCommandHandler<TEntity, TDto> : IRequestHandler<UpdateEntityCommand<TDto>, TDto> where TEntity : EntityBase
    
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEntityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TDto> Handle(UpdateEntityCommand<TDto> request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<TEntity>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {request.Id} not found.");
        }

        _mapper.Map(request.Dto, entity);
        await repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<TDto>(entity);
    }
}

