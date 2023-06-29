using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext
{
	public class AnContext : DbContext
	{
		//public DataContext(DbContextOptions options) : base(options)
		//{

		//}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Account> Accounts { get; set; }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetails> OrderDetails { get; set; }





		public AnContext(){}

		public AnContext(DbContextOptions<AnContext> options) : base(options) {}
	}
}
