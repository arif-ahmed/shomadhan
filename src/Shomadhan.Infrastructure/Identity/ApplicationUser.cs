using Microsoft.AspNetCore.Identity;

namespace Shomadhan.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? ShopId { get; set; }     
    public virtual Shop? Shop { get; set; }
}
