using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using Models.ProductModels;

namespace BusinessLogic.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}


		public async Task<List<ProductDto>> GetAll()
		{
			var products = await _productRepository.GetAll();
			
			var productsDto = products.Select(x => new ProductDto
				{
					ProductId = x.ProductId,
					ProductName = x.ProductName,
					ProductDescription = x.ProductDescription,
					Price = x.Price,
					CategoryName = x.Category.Name,
					Discontinued = x.Discontinued
				}).ToList();

			return productsDto;
		}

		public async Task<ProductDto> GetById(int productId)
		{
			var product = await _productRepository.GetById(productId);

			var productDto = new ProductDto
			{
				ProductId = product.ProductId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				CategoryName = product.Category.Name,
				Discontinued = product.Discontinued
			};

			return productDto;
		}

		public async Task<ProductDto> GetByName(string name)
		{
			var product = await _productRepository.GetByName(name);

			var productDto = new ProductDto
			{
				ProductId = product.ProductId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				CategoryName = product.Category.Name,
				Discontinued = product.Discontinued
			};

			return productDto;
		}

		public async Task CreateProduct(CreateProductModel model)
		{
			var newProduct = new Product
			{
				ProductName = model.ProductName,
				ProductDescription = model.ProductDescription,
				Price = model.Price,
				Discontinued = false,
				CategoryId = model.CategoryId
			};

			await _productRepository.CreateProduct(newProduct);
		}

		public async Task DeleteProduct(int id)
		{
			await _productRepository.DeleteProduct(id);
		}

		public async Task UpdateProduct(int productId, UpdateProductModel model)
		{
			await _productRepository.UpdateProduct(productId, model);
		}

		public async Task<List<CategoryDto>> GetCategories()
		{
			var categories = await _productRepository.GetCategories();


			var categoriesDto = categories.Select(x => new CategoryDto
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();

			return categoriesDto;
		}

		public async Task<CategoryDto> GetCategoryById(int categoryId)
		{
			var category = await _productRepository.GetCategoryById(categoryId);

			var categoryDto = new CategoryDto
			{
				Id = category.Id,
				Name = category.Name
			};

			return categoryDto;
		}

		public async Task CreateCategory(CreateCategoryModel model)
		{
			var newCategory = new Category
			{
				Name = model.Name
			};

			await _productRepository.CreateCategory(newCategory);
		}

		public async Task DeleteCategory(int id)
		{
			await _productRepository.DeleteCategory(id);

		}

		public async Task UpdateCategory(int categoryId, UpdateCategoryModel model)
		{
			await _productRepository.UpdateCategory(categoryId, model);
		}
	}
}
