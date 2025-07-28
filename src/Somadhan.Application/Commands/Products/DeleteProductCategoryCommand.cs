using MediatR;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Products;

public class DeleteProductCategoryCommand : IRequest<Unit>
{
    public string Id { get; set; }

    public DeleteProductCategoryCommand(string id)
    {
        Id = id;
    }
}

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, Unit>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCategoryCommandHandler(IProductCategoryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id);

        if (category == null)
        {
            // Handle not found scenario, e.g., throw an exception
            throw new Exception($"Product category with ID {request.Id} not found.");
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
