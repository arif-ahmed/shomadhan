using System.Linq.Expressions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Interfaces;
using Somadhan.Infrastructure.Common;
using Somadhan.Infrastructure.Data;
using Somadhan.Infrastructure.Identity;

namespace Somadhan.Infrastructure.Repositories;
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

    public async override Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users
            .Include(u => u.Shop) 
            .AsNoTracking()
            .Select(user => user.ToDomainUser())
            .ToListAsync(cancellationToken);

        return users;
    }

    public async override Task<(IEnumerable<User>, int)> FindAsync(Expression<Func<User, bool>>? predicate = null, int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        var dbset = _userManager.Users.AsQueryable();

        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
        }

        var appUserPredicate = ExpressionConversionHelper.ConvertPredicate<User, ApplicationUser>(predicate);

        var query = dbset.Where(appUserPredicate);

        int totalCount = await query.CountAsync();

        if (pageNumber > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize);
        }
        if (pageNumber > 0)
        {
            query = query.Take(pageSize);
        }

        var applicationUsers = await query.ToListAsync(cancellationToken);
        var users = applicationUsers.Select(user => user.ToDomainUser());

        return (users, totalCount);
    }
}
