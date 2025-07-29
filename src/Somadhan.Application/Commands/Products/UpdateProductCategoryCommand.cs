using MediatR;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Commands.Products;

public class UpdateProductCategoryCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ParentId { get; set; }

    public UpdateProductCategoryCommand(string id, string name, string? description = null, string? parentId = null)
    {
        Id = id;
        Name = name;
        Description = description;
        ParentId = parentId;
    }
}

public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, Unit>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCategoryCommandHandler(IProductCategoryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id);

        if (category == null)
        {
            // Handle not found scenario, e.g., throw an exception
            throw new Exception($"Product category with ID {request.Id} not found.");
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.ParentId = request.ParentId;

        await _repository.UpdateAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
