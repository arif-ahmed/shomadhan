using MediatR;

using Shomadhan.Application.Dtos;
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Domain.Interfaces;

namespace Shomadhan.Application.Queries;
public class GetShopUsersQuery : IRequest<GetShopUsersQueryResponse>
{
    public string? ShopId { get; set; }
    public string? SearchText { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
}

public class GetShopUsersQueryHandler : IRequestHandler<GetShopUsersQuery, GetShopUsersQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetShopUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<GetShopUsersQueryResponse> Handle(GetShopUsersQuery request, CancellationToken cancellationToken)
    {
        // var users = await _userRepository.GetUsersByShopIdAsync(request.ShopId, request.SearchText, request.PageNumber, request.PageSize);
        // var totalCount = await _userRepository.GetTotalCountByShopIdAsync(request.ShopId, request.SearchText);

        //var response = await _unitOfWork.UserRepository.FindAsync(user => user.ShopId == request.ShopId && 
        //    (string.IsNullOrEmpty(request.SearchText) || 
        //     user.UserName.Contains(request.SearchText) || 
        //     user.Email.Contains(request.SearchText)), 
        //    request.PageNumber, 
        //    request.PageSize, 
        //    cancellationToken: cancellationToken);

        var datra = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken: cancellationToken);

        // var projectedQuery = _userManager.Users.ProjectTo<User>(_mapper.ConfigurationProvider);


        var response = await _unitOfWork.UserRepository.FindAsync(user => user.ShopId == request.ShopId,
            request.PageNumber,
            request.PageSize,
            cancellationToken: cancellationToken);

        var userDtos = response.Item1.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ShopId = user.ShopId
        }).ToList();

        return new GetShopUsersQueryResponse
        {
            Users = userDtos,
            TotalCount = response.Item2
        };
    }
}

public class GetShopUsersQueryResponse
{
    public List<UserDto> Users { get; set; } = new List<UserDto>();
    public int TotalCount { get; set; }
}
