using MediatR;
using Somadhan.Domain.Interfaces;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Commands.Products;
public class CreateProductCategoryCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ParentCategoryId { get; set; }
    public CreateProductCategoryCommand(string name, string description, string? parentCategoryId = null)
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, string>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProductCategoryCommandHandler(IProductCategoryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<string> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new ProductCategory
        {
            Name = request.Name,
            Description = request.Description,
            ParentId = request.ParentCategoryId
        };

        await _unitOfWork.ProductCategoryRepository.AddAsync(category);
        await _unitOfWork.CommitAsync(cancellationToken);

        await _repository.AddAsync(category);
        return category.Id;
    }
}

