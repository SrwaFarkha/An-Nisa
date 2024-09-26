using DatabaseModels.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModels.DataContext
{
    public class AnContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

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
