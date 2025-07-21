namespace Somadhan.Domain.Core.MultiTenancy
{
    public interface IMayHaveTenant
    {
        public string? ShopId { get; set; }
        Shop? Shop { get; set; }        
    }
}
