using Somadhan.Domain.Core.MultiTenancy;

namespace Somadhan.Domain.Core.Identity;

public class User : EntityBase, IMayHaveTenant
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public string? ShopId { get; set; }

    #region Navigation Properties
    public Role Role { get; set; } = default!;
    public virtual Shop? Shop { get; set; }
    #endregion
}
