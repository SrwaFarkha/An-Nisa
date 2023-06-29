using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Models.AccountModels;
using Models.ProductModels;

namespace An_Nisa.WebApi.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAccounts()
		{
			var data = await _accountService.GetAllAccounts();

			return Ok(data);
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateAccount(CreateAccountModel model)
		{
			await _accountService.CreateAccount(model);
			return Ok();
		}

		[HttpPut("{accountId:int}/update")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAccount(int accountId, UpdateAccountModel model)
		{
			await _accountService.UpdateAccount(accountId, model);
			return Ok();
		}

		[HttpGet("{accountId:int}", Name = "GetAccountById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAccountById(int accountId)
		{
			var data = await _accountService.GetAccountById(accountId);
			return Ok(data);
		}

		[HttpGet("address/get-addresses")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAddresses()
		{
			var data = await _accountService.GetAddresses();
			return Ok(data);
		}

		[HttpGet("address/{addressId:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAddressById(int addressId)
		{
			var data = await _accountService.GetAddressById(addressId);
			return Ok(data);
		}

		[HttpPost("address/create-address")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateAddress(CreateAddressModel model)
		{
			await _accountService.CreateAddress(model);
			return Ok();
		}

		[HttpDelete("address/{addressId}/delete-address")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteAddress(int addressId)
		{
			await _accountService.DeleteAddress(addressId);
			return Ok();
		}

		[HttpPut("address/{addressId:int}/update-address")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAddress(int addressId, UpdateAddressModel model)
		{
			await _accountService.UpdateAddress(addressId, model);
			return Ok();
		}

		[HttpGet("shoppingcart/{accountId}", Name = "GetShoppingCartByAccountId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetShoppingCartByAccountId(int accountId)
		{
			var data = await _accountService.GetShoppingCartByAccountId(accountId);

			return Ok(data);
		}


		[HttpPost("shoppingcart/add", Name = "AddProductToShoppingCart")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> AddProductToShoppingCart([FromBody] AddProductToShoppingCartModel model)
		{
			var addedProductToShoppingCart = await _accountService.AddProductToShoppingCart(model);

			return Ok(addedProductToShoppingCart);
		}



	}
}
