using System.Linq.Expressions;

using MediatR;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Queries;
public class GetRolesQuery : IRequest<(IEnumerable<RoleDto>, int)>
{
    public string? SearchText { get; set; }
    public string? ShopId { get;set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, (IEnumerable<RoleDto>, int)>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetRolesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<(IEnumerable<RoleDto>, int)> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Role, bool>> predicate = role => true;

        var response = await _unitOfWork.RoleRepository.FindAsync(predicate, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);

        var roleDtos = response.Item1.Select(role => new RoleDto
        {
            Id = role.Id,
            Name = role.Name
        }).ToList();

        return (roleDtos, response.Item2);
    }
}
