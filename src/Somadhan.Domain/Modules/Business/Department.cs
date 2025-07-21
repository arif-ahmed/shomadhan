using Somadhan.Domain.Core.MultiTenancy;

namespace Somadhan.Domain.Modules.Business;

public class Department : EntityBase, IMustHaveTenant
{
    public string Name { get; set; } = default!;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
    public string TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
