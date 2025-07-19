using Shomadhan.Domain.Core.MultiTenancy;

namespace Shomadhan.Domain.Core.Identity;

public class Role : EntityBase, IMayHaveTenant
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ShopId { get; set; }

    #region Navigation Properties
    public virtual Shop? Shop { get; set; }
    #endregion
}
