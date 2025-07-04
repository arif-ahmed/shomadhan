using Shomadhan.Domain.Core.MultiTenancy;
using Shomadhan.Domain.Modules.Order;

namespace Shomadhan.Domain.Modules.Invoice;

public class Invoice : EntityBase, IMustHaveTenant
{
    public int OrderId { get; set; }
    public Order.Order Order { get; set; } = default!;
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; } = new();
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
