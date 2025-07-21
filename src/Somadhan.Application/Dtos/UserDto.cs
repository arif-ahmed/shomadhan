namespace Somadhan.Application.Dtos;
public class UserDto
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public string? ShopId { get; set; } = default!;
    public string? RoleName { get; set; }
    public string? RoleId { get; set; }
}
