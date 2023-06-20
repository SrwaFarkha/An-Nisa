using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ProductModels;

namespace BusinessLogic.Interfaces
{
    public interface IProductService
	{
		Task<List<ProductDto>> GetAll();
		Task<ProductDto> GetById(int productId);
		Task<ProductDto> GetByName(string name);
		Task CreateProduct(CreateProductModel model);
		Task DeleteProduct(int id);
		Task UpdateProduct(int productId, UpdateProductModel model);
		Task<List<CategoryDto>> GetCategories();
		Task<CategoryDto> GetCategoryById(int categoryId);
		Task CreateCategory(CreateCategoryModel model);
		Task DeleteCategory(int id);
		Task UpdateCategory(int categoryId, UpdateCategoryModel model);
	}
}
