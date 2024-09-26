using DatabaseModels.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModels.DataContext
{
    public class AnContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<SizeStock> SizeStocks { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }


        // Default constructor
        public AnContext() { }

        // Constructor for dependency injection
        public AnContext(DbContextOptions<AnContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
