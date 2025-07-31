using Microsoft.AspNetCore.Identity;

namespace Somadhan.Persistence.EF;

public class ApplicationUser : IdentityUser
{
    public string? ShopId { get; set; }
    public virtual Shop? Shop { get; set; }    
}
