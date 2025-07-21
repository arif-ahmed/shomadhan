using System.Linq.Expressions;

using MediatR;

using Somadhan.Application.Dtos;
using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Interfaces;

namespace Somadhan.Application.Queries;
public class GetUsersQuery : IRequest<(IEnumerable<UserDto>, int)>
{
    public string? SearchText { get; set; }
    public string? ShopId { get; set; }   
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, (IEnumerable<UserDto>, int)>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<(IEnumerable<UserDto>, int)> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>>? predicate = u => true;

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            predicate = user => user.UserName.Contains(request.SearchText) || user.Email.Contains(request.SearchText);
        }

        if (!string.IsNullOrEmpty(request.ShopId))
        {
            predicate = user => user.ShopId == request.ShopId;
        }

        var response = await _unitOfWork.UserRepository.FindAsync(predicate, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);

        var userDtos = response.Item1.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ShopId = user.ShopId
        }).ToList();

        return (userDtos, response.Item2);
    }
}
