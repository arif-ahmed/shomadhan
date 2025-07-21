using Somadhan.Domain.Core.MultiTenancy;
using Somadhan.Domain.Modules.Order;

namespace Somadhan.Domain.Modules.Invoice;

public class Invoice : EntityBase, IMustHaveTenant
{
    public int OrderId { get; set; }
    public Order.Order Order { get; set; } = default!;
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; } = new();
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
