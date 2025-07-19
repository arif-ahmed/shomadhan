namespace Shomadhan.Domain.Modules.Product;
public class ProductCategory : EntityBase
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ParentId { get; set; }
    public ProductCategory? Parent { get; set; }
    public ICollection<ProductCategory> Children { get; set; } = new List<ProductCategory>();
    public ICollection<ProductDetails> Products { get; set; } = new List<ProductDetails>();
}
