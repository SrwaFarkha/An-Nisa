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
		public AnContext(){}

		public AnContext(DbContextOptions<AnContext> options) : base(options) {}
	}
}
