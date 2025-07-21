namespace Somadhan.Application.Dtos;
public class RoleDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ShopId { get; set; } = default!;
    // You can add more properties if needed
}
