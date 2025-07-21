using Somadhan.Domain.Core.MultiTenancy;

namespace Somadhan.Domain.Modules.Business;

public class Branch : EntityBase, IMustHaveTenant
{
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
