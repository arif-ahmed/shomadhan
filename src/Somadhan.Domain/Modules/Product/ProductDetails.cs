using Somadhan.Domain.Core.MultiTenancy;

namespace Somadhan.Domain.Modules.Product;

public class ProductDetails : EntityBase, IMayHaveTenant
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public string? ShopId { get; set; }
    public virtual Shop? Shop { get; set; }
}
