using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Models.ProductModels;

namespace DataAccess.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly DataContext.AnContext _dbContext;

		public ProductRepository(DataContext.AnContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Product>> GetAll()
		{
			var products = await _dbContext.Products
				.Include(x => x.Images)
				.Include(x => x.Category)
				.ToListAsync();

			return products;

		}

		public async Task<Product> GetById(int productId)
		{
			var product = await _dbContext.Products
				.Include(x => x.Sizes)
				.Include(x => x.Images)
				.Include(x => x.Category)
				.Include(x => x.ProductDetails)
				.FirstOrDefaultAsync(x => x.ProductId == productId);

			return product;
		}

		public async Task<Product> GetByName(string name)
		{
			var product = await _dbContext.Products
				.Include(x => x.Images)
				.Include(x => x.Category)
				.Include(x => x.ProductDetails)
				.FirstOrDefaultAsync(x => x.ProductName == name);

			return product;
		}

		public async Task CreateProduct(Product product)
		{
			await _dbContext.Products.AddAsync(product);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteProduct(int productId)
		{
			var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
			_dbContext.Products.Remove(product);

			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateProduct(int productId, UpdateProductModel model)
		{
			var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

			if (productFromDb != null)
			{
				productFromDb.ProductName = model.ProductName;
				productFromDb.Price = model.Price;
				productFromDb.CategoryId = model.CategoryId;
				productFromDb.Discontinued = model.Discontinued;


				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task<List<Category>> GetCategories()
		{
			var categories = await _dbContext.Categories.ToListAsync();
			return categories;
		}

		public async Task<Category> GetCategoryById(int categoryId)
		{
			var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
			
			return category;
		}

		public async Task CreateCategory(Category category)
		{
			await _dbContext.Categories.AddAsync(category);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteCategory(int id)
		{
			var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

			_dbContext.Categories.Remove(category);

			await _dbContext.SaveChangesAsync();

		}

		public async Task UpdateCategory(int categoryId, UpdateCategoryModel model)
		{
			var categoryFromDb = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id ==categoryId);

			if (categoryFromDb != null)
			{
				categoryFromDb.Name = model.Name;
			}

			await _dbContext.SaveChangesAsync();
		}

		public async Task<List<Comment>> GetCommentsByProductId(int productId)
		{
			var comments = await _dbContext.Comments
				.Where(x => x.ProductId == productId)
				.ToListAsync();

			return comments;
		}

		public async Task CreateComment(Comment newComment)
		{
			await _dbContext.Comments.AddAsync(newComment);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteComment(int commentId)
		{
			var comment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

			_dbContext.Comments.Remove(comment);

			await _dbContext.SaveChangesAsync();
		}
	}
}
