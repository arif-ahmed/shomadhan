using System.Linq.Expressions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Interfaces;
using Somadhan.Infrastructure.Common;
using Somadhan.Infrastructure.Data;

namespace Somadhan.Infrastructure.Repositories;
public class IdentityRoleRepository : EntityRepository<Role>, IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public IdentityRoleRepository(AppDbContext context, RoleManager<IdentityRole> roleManager) : base(context)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager), "RoleManager cannot be null.");
    }

    public override async Task<Role?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be null or empty.");
        }
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with id {id} not found.");
        }
        return new Role
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        };
    }

    public override async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleManager.Roles.AsNoTracking().Select(role => new Role
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
        }).ToListAsync();

        return roles;
    }

    public override async Task<(IEnumerable<Role>, int)> FindAsync(Expression<Func<Role, bool>>? predicate = null, int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        var dbset = _roleManager.Roles.AsQueryable();

        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
        }

        var appRolePredicate = ExpressionConversionHelper.ConvertPredicate<Role, IdentityRole>(predicate);
        var query = dbset.Where(appRolePredicate);

        int totalCount = await query.CountAsync();

        if (pageNumber > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize);
        }
        if (pageNumber > 0)
        {
            query = query.Take(pageSize);
        }

        var applicationRoles = await query.ToListAsync(cancellationToken);
        var roles = applicationRoles.Select(role => new Role { Id = role.Id, Name = role?.Name });

        return (roles, totalCount);
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

    public override async Task UpdateAsync(Role entity, CancellationToken cancellationToken = default)
    {
        var role = await _roleManager.FindByIdAsync(entity.Id);

        if (role == null)
        { 
            throw new KeyNotFoundException($"Role with ID {entity.Id} not found.");
        }

        if(!string.IsNullOrWhiteSpace(entity.Name))
        {
            role.Name = entity.Name;
        }

        await _roleManager.UpdateAsync(role);
    }

    public override async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Role ID cannot be null or empty.", nameof(id));
        }
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {id} not found.");
        }
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to delete role with ID {id}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}
