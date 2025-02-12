using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using SharedModels.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SharedModels.Enums;

namespace BusinessLogic.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;


        public JwtService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;

        }

        public async Task<string?> GetToken(LoginDto login)
        {
            var user = await _accountRepository.GetAccountByEmail(login.Email);

            // Compare passwords (assumes stored passwords are hashed)
            if (user != null && VerifyPassword(login.Password, user.Password))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                new Claim("accountId", user.AccountId.ToString())
                };


                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }

            return null;
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Replace with your preferred password hashing library
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }
    }
}
