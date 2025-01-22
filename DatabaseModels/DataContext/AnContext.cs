using DatabaseModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<ProductInformation> ProductInformations { get; set; }
        public DbSet<CareAndAdvice> CareAndAdvices { get; set; }





        // Constructor for dependency injection
        public AnContext(DbContextOptions<AnContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CareAndAdvice>()
               .HasOne(ca => ca.ProductDetails)
               .WithOne(pd => pd.CareAndAdvice)
               .HasForeignKey<CareAndAdvice>(ca => ca.ProductDetailsId);

            modelBuilder.Entity<ProductInformation>()
               .HasOne(pi => pi.ProductDetails)
               .WithOne(pd => pd.ProductInformation)
               .HasForeignKey<ProductInformation>(pi => pi.ProductDetailsId); 

            modelBuilder.Entity<Description>()
                .HasOne(d => d.ProductDetails)
                .WithOne(pd => pd.Description)
                .HasForeignKey<Description>(d => d.ProductDetailsId);

        }
    }
}