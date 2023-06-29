
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ProductModels;

namespace An_Nisa.WebApi.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetProducts()
		{
			var data = await _productService.GetAll();

			return Ok(data);
		}


		[HttpGet("{productId:int}", Name = "GetProductById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetProductById(int productId)
		{
			var data = await _productService.GetById(productId);
			return Ok(data);
		}

		[HttpGet("{name}", Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetProduct(string name)
		{
			var data = await _productService.GetByName(name);
			return Ok(data);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateProduct(CreateProductModel model)
		{
			await _productService.CreateProduct(model);
			return Ok();
		}

		[HttpDelete("{productId}/delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteProduct(int productId)
		{
			await _productService.DeleteProduct(productId);
			return Ok();
		}

		[HttpPut("{productId:int}/update")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateProduct(int productId, UpdateProductModel model)
		{
			await _productService.UpdateProduct(productId, model);
			return Ok();
		}

		[HttpGet("category/get-categories")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCategories()
		{
			var data = await _productService.GetCategories();
			return Ok(data);
		}

		[HttpGet("category/{categoryId:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCategoryById(int categoryId)
		{
			var data = await _productService.GetCategoryById(categoryId);
			return Ok(data);
		}

		[HttpPost("category/create-category")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
		{
			await _productService.CreateCategory(model);
			return Ok();
		}

		[HttpDelete("category/{categoryId}/delete-category")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteCategory(int categoryId)
		{
			await _productService.DeleteCategory(categoryId);
			return Ok();
		}

		[HttpPut("category/{categoryId:int}/update-category")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateCategory(int categoryId, UpdateCategoryModel model)
		{
			await _productService.UpdateCategory(categoryId, model);
			return Ok();
		}

		[HttpGet("comment/{productId:int}", Name = "GetCommentsByProductId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetCommentsByProductId(int productId)
		{
			var data = await _productService.GetCommentsByProductId(productId);
			return Ok(data);
		}

		[HttpPost("comment/create-comment/{productId:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateComment(int productId, CreateCommentModel model)
		{
			await _productService.CreateComment(productId, model);
			return Ok();
		}

		[HttpDelete("comment/{commentId}/delete-comment")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteComment(int commentId)
		{
			await _productService.DeleteComment(commentId);
			return Ok();
		}

	}
}
