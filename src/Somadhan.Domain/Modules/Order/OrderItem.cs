using Somadhan.Domain.Core.MultiTenancy;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Domain.Modules.Order;

public class OrderItem : EntityBase, IMustHaveTenant
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public int ProductId { get; set; }
    public Product.ProductDetails Product { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
