namespace Shomadhan.API.Models;
public class CreateShopRequest
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpeningHours { get; set; }
    public string? ClosingHours { get; set; }
    public string? OwnerName { get; set; }
}
