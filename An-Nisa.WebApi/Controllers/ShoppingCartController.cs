using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DatabaseModels.DatabaseEnums;
using Microsoft.AspNetCore.Mvc;
using SharedModels.ShoppingCartModels;

namespace An_Nisa.WebApi.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]

    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        [HttpGet("{accountId}", Name = "GetShoppingCartByAccountId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShoppingCartByAccountId(int accountId)
        {
            var data = await _shoppingCartService.GetShoppingCartByAccountId(accountId);

            return Ok(data);
        }


        [HttpPost("add", Name = "AddProductToShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductToShoppingCart([FromBody] AddProductToShoppingCartModel model)
        {
            var addedProductToShoppingCart = await _shoppingCartService.AddProductToShoppingCart(model);

            return Ok(addedProductToShoppingCart);
        }

        [HttpPost("empty/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmptyShoppingCart(int accountId)
        {
            var result = await _shoppingCartService.EmptyShoppingCart(accountId);
            return Ok(result);
        }

        [HttpPost("increase/{accountId}/{productId}/{size}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IncreaseQuantity(int accountId, int productId, DatabaseEnums.Size size)
        {
            var result = await _shoppingCartService.IncreaseShoppingCartProduct(accountId, productId, size);

            if (result == false)
            {
                return NotFound("Product could not be increased!");
            }

            return Ok("Product increased!");
        }

        [HttpPost("decrease/{accountId}/{productId}/{size}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DecreaseQuantity(int accountId, int productId, DatabaseEnums.Size size)
        {
            var result = await _shoppingCartService.DecreaseShoppingCartProduct(accountId, productId, size);
            if (result == false)
            {
                return NotFound("Product could not be decreased!");
            }

            return Ok("Product decreased!");
        }

        [HttpPost("delete-cartitem/{accountId}/{productId}/{size}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size)
        {
            var result = await _shoppingCartService.DeleteCartItemFromShoppingCart(accountId, productId, size);
            return Ok(result);

        }
    }
}
