namespace ShopSphere.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public required string Category { get; set; }
        // Add other properties as needed
    }
}
