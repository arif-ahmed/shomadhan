using Somadhan.Domain.Core.Identity;
using Somadhan.Domain.Core.MultiTenancy;
using Somadhan.Domain.Modules.Business;

namespace Somadhan.Domain.Modules.Order;

public class Order : EntityBase, IMustHaveTenant
{
    public int CustomerId { get; set; }
    public User Customer { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
