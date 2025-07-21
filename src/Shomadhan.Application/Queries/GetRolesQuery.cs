using MediatR;
using Shomadhan.Application.Dtos;
using Shomadhan.Domain.Interfaces;

namespace Shomadhan.Application.Queries;
public class GetRolesQuery : IRequest<List<RoleDto>>
{
    public string? ShopId { get; set; }
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetRolesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.RoleRepository.GetAllAsync(cancellationToken);
        return roles.Select(role => new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role?.Description ?? string.Empty,
            ShopId = role?.ShopId ?? string.Empty,
        }).ToList();
    }
}
