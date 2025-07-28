using MediatR;

using Somadhan.Application.Dtos;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Queries;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public string Id { get; set; }
    public GetRoleByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {request.Id} not found.");
        }

        return new RoleDto
        {
            Id = role.Id,
            Name = role?.Name
        };
    }
}

