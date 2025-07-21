namespace Somadhan.Domain.Core.MultiTenancy;

public class Tenant : EntityBase
{
    public string Name { get; set; } = default!;
    public string? ConnectionString { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidUpTo { get; set; }
}
