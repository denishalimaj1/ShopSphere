namespace ShopSphere.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        
        // Navigation properties
        public required User User { get; set; }
        public required ICollection<CartItem> CartItems { get; set; }
    }
}
