using MediatR;

using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Commands.Identity;
public class UpdateRoleCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShopId { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {request.Id} not found.");
        }

        if(!string.IsNullOrWhiteSpace(request.Name))
        {
            role.Name = request.Name;
        }

        if(!string.IsNullOrWhiteSpace(request.Description))
        {
            role.Description = request.Description;
        }

        if (!string.IsNullOrWhiteSpace(request.ShopId))
        {
            role.ShopId = request.ShopId;
        }

        await _unitOfWork.RoleRepository.UpdateAsync(role, cancellationToken);
        // await _unitOfWork.CommitAsync(cancellationToken);
    }
}
