using Somadhan.Domain.Core.MultiTenancy;

namespace Somadhan.Domain.Modules.Product;
public class Brand : EntityBase, IMayHaveTenant
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ShopId { get; set; }

    #region Navigation Properties
    public virtual Shop? Shop { get; set; }
    public ICollection<ProductDetails> Products { get; set; } = new List<ProductDetails>();
    #endregion
}
