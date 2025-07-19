
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Infrastructure.Identity;

namespace Shomadhan.Infrastructure.Common;
public static class UserMappingExtensions
{
    public static User ToDomainUser(this ApplicationUser appUser)
    {
        return new User
        {

        };
    }

    public static ApplicationUser ToApplicationUser(this User user)
    {
        return new ApplicationUser
        {

        };
    }
}

