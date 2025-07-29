namespace Somadhan.Application.Dtos;

public class ProductCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ParentId { get; set; }
    public string? ShopId { get; set; }
}
