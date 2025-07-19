
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Infrastructure.Identity;

namespace Shomadhan.Infrastructure.Common;
public static class UserMappingExtensions
{
    public static User ToDomainUser(this ApplicationUser appUser)
    {
        return new User
        {
            Id = appUser.Id,
            UserName = appUser?.UserName ?? string.Empty,
            Email = appUser?.Email ?? string.Empty,
            ShopId = appUser?.ShopId ?? string.Empty
            // Map other properties as needed

        };
    }

    public static ApplicationUser ToApplicationUser(this User user)
    {
        return new ApplicationUser
        {

        };
    }
}

