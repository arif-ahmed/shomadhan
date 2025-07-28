namespace Somadhan.Application.Dtos
{
    public class ProductDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductCategoryId { get; set; }
        public string ShopId { get; set; }
    }
}
