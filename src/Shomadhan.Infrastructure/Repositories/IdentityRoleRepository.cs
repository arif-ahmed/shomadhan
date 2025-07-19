using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shomadhan.Domain.Core.Identity;
using Shomadhan.Domain.Interfaces;
using Shomadhan.Infrastructure.Data;

namespace Shomadhan.Infrastructure.Repositories;
public class IdentityRoleRepository : EntityRepository<Role>, IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public IdentityRoleRepository(AppDbContext context, RoleManager<IdentityRole> roleManager) : base(context)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager), "RoleManager cannot be null.");
    }

    public override async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleManager.Roles.Select(role => new Role
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
        }).ToListAsync();

        return roles;
    }

    public virtual async Task AddAsync(Role role)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role), "Role cannot be null.");
        }

        var identityRole = new IdentityRole
        {
            Name = role.Name,
            // Map other properties as needed
        };

        var result = await _roleManager.CreateAsync(identityRole);
    }
}
