using MediatR;
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Domain.Interfaces;

namespace Shomadhan.Application.Commands.Identity;
public class CreateRoleCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string ShopId { get; set; } = default!;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Role name is required.", nameof(request.Name));

        var role = new Role 
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Description = request.Description,
            ShopId = request.ShopId
        };

        await _unitOfWork.RoleRepository.AddAsync(role);
    }
}
