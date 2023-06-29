

using DataAccess.Models;
using Models.ProductModels;

namespace DataAccess.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAll();
		Task<Product> GetById(int productId);
		Task<Product> GetByName(string name);
		Task CreateProduct(Product product);
		Task DeleteProduct(int id);
		Task UpdateProduct(int productId, UpdateProductModel model);
		Task<List<Category>> GetCategories();
		Task<Category> GetCategoryById(int categoryId);
		Task CreateCategory(Category category);
		Task DeleteCategory(int id);
		Task UpdateCategory(int categoryId, UpdateCategoryModel model);
		Task<List<Comment>> GetCommentsByProductId(int productId);
		Task CreateComment(Comment newComment);
		Task DeleteComment(int commentId);
	}
}
