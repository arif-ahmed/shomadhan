namespace Somadhan.Domain.Core.MultiTenancy
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
