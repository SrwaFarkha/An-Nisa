using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedModels.AccountModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace An_Nisa.WebApi.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAccountService _accountService;

        public TokenController(IJwtService jwtService, IAccountService accountService)
        {
            _jwtService = jwtService;
            _accountService = accountService;
        }



        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> GetToken([FromBody] LoginDto login)
        {
            var token = await _jwtService.GetToken(login);

            if (!String.IsNullOrEmpty(token))
            {
                var tokenResponse = new { token = token };

                return Ok(tokenResponse);
            }
            

            return Unauthorized();

        }


    }
}
