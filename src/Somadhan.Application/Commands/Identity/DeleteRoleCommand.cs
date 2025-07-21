using MediatR;

using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Identity;
public class DeleteRoleCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string? ShopId { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Id))
            throw new ArgumentException("Role ID is required.", nameof(request.Id));
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {request.Id} not found.");
        }
        await _unitOfWork.RoleRepository.DeleteAsync(role.Id, cancellationToken);
    }
}
