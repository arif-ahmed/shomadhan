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
        Expression<Func<User, bool>> predicate = u => true;

        // SearchText filter
        if (!string.IsNullOrEmpty(request.SearchText))
        {
            var searchText = request.SearchText.ToLower();
            predicate = predicate.AndAlso(user =>
                user.UserName.ToLower().Contains(searchText) ||
                user.Email.ToLower().Contains(searchText));
        }

        // ShopId filter
        if (!string.IsNullOrEmpty(request.ShopId))
        {
            predicate = predicate.AndAlso(user => user.ShopId == request.ShopId);
        }

        var response = await _unitOfWork.UserRepository.FindAsync(
            predicate, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);

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

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));
        var combined = Expression.AndAlso(
            Expression.Invoke(expr1, parameter),
            Expression.Invoke(expr2, parameter));
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}
