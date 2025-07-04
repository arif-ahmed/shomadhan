namespace Shomadhan.API.Models;
public class RegisterDto
{
    public string? Email { get; set; }
    public required string Password { get; set; }
    public int ShopId { get; set; }
}