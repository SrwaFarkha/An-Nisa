using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Identity.Client;
using Models.AccountModels;
using Models.ProductModels;

namespace BusinessLogic.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}
		public async Task<List<AccountDto>> GetAllAccounts()
		{
			var accounts = await _accountRepository.GetAllAccounts();

			var accountsDto = accounts.Select(x => new AccountDto
			{
				AccountId = x.AccountId,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
				PhoneNumber = x.PhoneNumber,
				CreatedOn = x.CreatedOn,
				IsAdmin = x.IsAdmin,
				Password = x.Password,

				Address = new AddressDto
				{
					Country = x.Address.Country,
					City = x.Address.City,
					PostNumber = x.Address.PostNumber,
					StreetAddress = x.Address.StreetAddress,
				}
			}).ToList();

			return accountsDto;
		}

		public async Task CreateAccount(CreateAccountModel model)
		{
			var newAccount = new Account
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				CreatedOn = DateTime.Now,
				Password = model.Password,
				Address = new Address
				{
					Country = model.Address.Country,
					City = model.Address.City,
					PostNumber = model.Address.PostNumber,
					StreetAddress = model.Address.StreetAddress,
				}
			};

			await _accountRepository.CreateAccount(newAccount);
		}

		public async Task UpdateAccount(int accountId, UpdateAccountModel model)
		{
			await _accountRepository.UpdateAccount(accountId, model);

		}

		public async Task<AccountDto> GetAccountById(int accountId)
		{
			var account = await _accountRepository.GetAccountById(accountId);

			var accountDto = new AccountDto
			{
				AccountId = account.AccountId,
				FirstName = account.FirstName,
				LastName = account.LastName,
				Email = account.Email,
				PhoneNumber = account.PhoneNumber,
				CreatedOn = account.CreatedOn,
				IsAdmin = account.IsAdmin,
				Password = account.Password,
				Address = new AddressDto
				{
					Country = account.Address.Country,
					City = account.Address.City,
					PostNumber = account.Address.PostNumber,
					StreetAddress = account.Address.StreetAddress,
				}
			};

			return accountDto;
		}

		public async Task<List<AddressDto>> GetAddresses()
		{
			var addresses = await _accountRepository.GetAddresses();


			var addressesDto = addresses.Select(x => new AddressDto
			{
				Country = x.Country,
				City = x.City,
				PostNumber = x.PostNumber,
				StreetAddress = x.StreetAddress,
			}).ToList();

			return addressesDto;
		}

		public async Task<AddressDto> GetAddressById(int addressId)
		{
			var address = await _accountRepository.GetAddressById(addressId);

			var addressDto = new AddressDto
			{
				Country = address.Country,
				City = address.City,
				PostNumber = address.PostNumber,
				StreetAddress = address.StreetAddress,
			};

			return addressDto;
		}

		public async Task CreateAddress(CreateAddressModel model)
		{
			var newAddress = new Address
			{
				Country = model.Country,
				City = model.City,
				PostNumber = model.PostNumber,
				StreetAddress = model.StreetAddress,
			};

			await _accountRepository.CreateAddress(newAddress);
		}

		public async Task DeleteAddress(int addressId)
		{
			await _accountRepository.DeleteAccount(addressId);

		}

		public async Task UpdateAddress(int addressId, UpdateAddressModel model)
		{
			await _accountRepository.UpdateAddress(addressId, model);

		}

		public async Task<ShoppingCartDto> GetShoppingCartByAccountId(int accountId)
		{
			var shoppingCart = await _accountRepository.GetShoppingCartByAccountId(accountId);

			var shoppingCartDto = new ShoppingCartDto
			{
				Products = shoppingCart.CartItems.Select(item => new CartItemDto
				{
					ProductId = item.Product.ProductId,
					ProductName = item.Product.ProductName,
					Price = item.Product.Price,
					Quantity = item.Quantity,
					TotalProductPrice = item.Quantity * item.Product.Price
				}).ToList(),
				ShoppingCartTotalPrice = shoppingCart.CartItems.Sum(item => item.Quantity * item.Product.Price)
			};

			return shoppingCartDto;




			//var shoppingCart = await _accountRepository.GetShoppingCartByAccountId(accountId);

			//var shoppingCartDto = new ShoppingCartDto
			//{
			//	Products = shoppingCart.CartItems.Select(item => new CartItemDto
			//	{
			//		ProductId = item.Product.ProductId,
			//		ProductName = item.Product.ProductName,
			//		Price = item.Product.Price,
			//		Quantity = item.Quantity,
			//		TotalProductPrice = item.Quantity * item.Product.Price
			//	}).ToList(),
			//	ShoppingCartTotalPrice = shoppingCart.CartItems.Sum(item => item.Quantity * item.Product.Price)
			//};

			//return shoppingCartDto;



			//var shoppingCart = await _accountRepository.GetShoppingCartByAccountId(accountId);

			//var shoppingCartDto = new ShoppingCartDto
			//{
			//	Products = shoppingCart.CartItems.Select(shoppingCart => new CartItemDto
			//	{
			//		ProductId = shoppingCart.Product.ProductId,
			//		ProductName = shoppingCart.Product.ProductName,
			//		Price = shoppingCart.Product.Price,
			//		Quantity = shoppingCart.Quantity,
			//		TotalProductPrice = shoppingCart.Quantity * shoppingCart.Product.Price
			//	}),
			//	ShoppingCartTotalPrice = shoppingCart.CartItems.Select(x => x.Quantity * x.Product.Price).Sum()
			//};
			//return shoppingCartDto;

		}
	}
}
