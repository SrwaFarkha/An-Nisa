using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DatabaseModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedModels.AccountModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace An_Nisa.WebApi.Controllers
{
	[Route("api/account")]
	[ApiController]
	
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
			_accountService = accountService;
            _configuration = configuration;


        }

		[AllowAnonymous]
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginDto login)
		{
			var user = Authenticate(login);
			if (user != null)
			{
				var token = Generate(user);
				return Ok(token);
			}
			return NotFound("User not found");
		}

        private string Generate(AccountDto user)
        {
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
            };

			var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.UtcNow.AddMinutes(15),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private AccountDto? Authenticate(LoginDto login)
        {
            return _accountService.Authenticate(login.Email, login.Password);
		}


        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
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

		[HttpPost("shoppingcart/empty/{accountId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> EmptyShoppingCart(int accountId)
		{
			var result = await _accountService.EmptyShoppingCart(accountId);
			return Ok(result);
		}

		[HttpPost("shoppingcart/increase/{accountId}/{productId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> IncreaseQuantity(int accountId, int productId)
		{
			var result = await _accountService.IncreaseShoppingCartProduct(accountId, productId);
			return Ok(result);
		}

		[HttpPost("shoppingcart/decrease/{accountId}/{productId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DecreaseQuantity(int accountId, int productId)
		{
			var result = await _accountService.DecreaseShoppingCartProduct(accountId, productId);
			return Ok(result);
		}

		[HttpPost("shoppingcart/delete-cartitem/{accountId}/{productId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteCartItemFromShoppingCart(int accountId, int productId)
		{
			var result = await _accountService.DeleteCartItemFromShoppingCart(accountId, productId);
			return Ok(result);

		}

	}
}
