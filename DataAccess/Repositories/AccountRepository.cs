using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Models.AccountModels;

namespace DataAccess.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly DataContext.AnContext _dbContext;

		public AccountRepository(DataContext.AnContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<List<Account>> GetAllAccounts()
		{
			var accounts = await _dbContext.Accounts
				.Include(x => x.Address)
				.ToListAsync();

			return accounts;

		}

		public async Task CreateAccount(Account newAccount)
		{
			await _dbContext.Accounts.AddAsync(newAccount);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAccount(int accountId, UpdateAccountModel model)
		{
			var accountFromDb = await _dbContext.Accounts.Include(x => x.Address )
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			if (accountFromDb != null)
			{
				accountFromDb.FirstName = model.FirstName;
				accountFromDb.LastName = model.LastName;
				accountFromDb.Email = model.Email;
				accountFromDb.PhoneNumber = model.PhoneNumber;
				accountFromDb.Password = model.Password;
				accountFromDb.Address.Country = model.Address.Country;
				accountFromDb.Address.City = model.Address.City;
				accountFromDb.Address.PostNumber = model.Address.PostNumber;
				accountFromDb.Address.StreetAddress = model.Address.StreetAddress;

				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task<Account> GetAccountById(int accountId)
		{
			var account = await _dbContext.Accounts
				.Include(x => x.Address)
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			return account;
		}

		public async Task<List<Address>> GetAddresses()
		{
			var addresses = await _dbContext.Addresses.ToListAsync();
			return addresses;
		}

		public async Task<Address> GetAddressById(int addressId)
		{
			var address = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == addressId);

			return address;
		}

		public async Task CreateAddress(Address newAddress)
		{
			await _dbContext.Addresses.AddAsync(newAddress);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAccount(int addressId)
		{
			var address = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == addressId);

			_dbContext.Addresses.Remove(address);

			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAddress(int addressId, UpdateAddressModel model)
		{
			var addressFromDb = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == addressId);

			if (addressFromDb != null)
			{
				addressFromDb.Country = model.Country;
				addressFromDb.City = model.City;
				addressFromDb.PostNumber = model.PostNumber;
				addressFromDb.StreetAddress = model.StreetAddress;
			}

			await _dbContext.SaveChangesAsync();
		}


		public async Task<ShoppingCart> GetShoppingCartByAccountId(int accountId)
		{
			var shoppingCart = await _dbContext.ShoppingCarts
				.Include(x=> x.CartItems)
				.ThenInclude(x=>x.Product)
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			//.Select(x => new ShoppingCartDto
			//{
			//	ShoppingCartTotalPrice = x.CartItems.Select(x => x.Quantity * x.Product.Price).Sum(),
			//	Products = x.CartItems.Select(x => new CartItemDto
			//	{
			//		ProductId = x.Product.ProductId,
			//		ProductName = x.Product.ProductName,
			//		Price = x.Product.Price,
			//		Quantity = x.Quantity,
			//		TotalProductPrice = x.Quantity * x.Product.Price,
			//	}),
			//}).FirstOrDefaultAsync();


			return shoppingCart;
		}
	}
}
