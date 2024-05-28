namespace ShopSphere.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Foreign keys
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        
        // Navigation properties
        public required Product Product { get; set; }
        public required Order Order { get; set; }
    }
}
