namespace ECommercePortal.API.GraphQL.Product.Inputs
{
    public class CreateProductInput
    {
        public Guid ProductOwnerId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
