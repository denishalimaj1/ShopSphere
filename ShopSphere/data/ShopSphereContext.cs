using Microsoft.EntityFrameworkCore;
using ShopSphere.Models;

namespace ShopSphere.Data
{
    public class ShopSphereContext : DbContext
    {
        public ShopSphereContext(DbContextOptions<ShopSphereContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships if necessary
            // modelBuilder.Entity<Order>()
            //     .HasOne(o => o.User)
            //     .WithMany(u => u.Orders)
            //     .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Order)
                .WithMany(o => o.CartItems)
                .HasForeignKey(ci => ci.OrderId);

        
        }
    }
}
