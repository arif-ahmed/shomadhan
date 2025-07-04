namespace Shomadhan.Domain.Modules.Product;

public class ProductDetails : EntityBase
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }
}
