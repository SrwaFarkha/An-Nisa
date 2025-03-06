using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DatabaseModels.DatabaseEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SharedModels.AccountModels;


namespace An_Nisa.WebApi.Controllers
{
	[Route("api/account")]
	[ApiController]
	
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IJwtService _jwtService;
        public AccountController(IAccountService accountService, IJwtService jwtService)
        {
			_accountService = accountService;
			_jwtService = jwtService;
        }

        [Authorize]
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
            var accountCreated = await _accountService.CreateAccount(model);

            if (accountCreated)
            {
                var loginDto = new LoginDto { Email = model.Email, Password = model.Password };
                var token = await _jwtService.GetToken(loginDto);

                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new { token });
                }

                return BadRequest("Account created, but failed to generate token.");
            }

            return BadRequest("Account creation failed.");
        }




        [Authorize]
        [HttpPut("{accountId:int}/update")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAccount(int accountId, UpdateAccountModel model)
		{
			await _accountService.UpdateAccount(accountId, model);
			return Ok();
		}

		[Authorize]
		[HttpGet("{accountId:int}", Name = "GetAccountById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAccountById(int accountId)
		{
			var data = await _accountService.GetAccountById(accountId);
			return Ok(data);
		}

        [Authorize]
        [HttpGet("address/get-addresses")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAddresses()
		{
			var data = await _accountService.GetAddresses();
			return Ok(data);
		}

        [Authorize]
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

        [Authorize]
        [HttpDelete("address/{addressId}/delete-address")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteAddress(int addressId)
		{
			await _accountService.DeleteAddress(addressId);
			return Ok();
		}


        [Authorize]
        [HttpPut("address/{addressId:int}/update-address")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAddress(int addressId, UpdateAddressModel model)
		{
			await _accountService.UpdateAddress(addressId, model);
			return Ok();
		}
	}
}
