using Somadhan.Domain.Core.MultiTenancy;

namespace Somadhan.Domain.Modules.Business;

public class Organization : EntityBase, IMustHaveTenant
{
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
