using Microsoft.AspNetCore.Identity;
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure.Common;
using Shomadhan.Infrastructure.Data;
using Shomadhan.Infrastructure.Identity;

namespace Shomadhan.Infrastructure.Repositories;
public class IdentityUserRepository : EntityRepository<User>, IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public IdentityUserRepository(AppDbContext context, UserManager<ApplicationUser> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task AddAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        }

        var appUser = new ApplicationUser
        {
            UserName = user.FirstName,
            Email = user.Email,
            // Map other properties as needed
        };

        var result = await _userManager.CreateAsync(appUser, user.Password);
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var appUser = await _userManager.FindByIdAsync(id);

        if (appUser == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        return appUser.ToDomainUser();
    }
}
