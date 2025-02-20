using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using DatabaseModels.DataContext;
using DatabaseModels.Models;
using SharedModels.AccountModels;
namespace DataAccess.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly AnContext _dbContext;

		public AccountRepository(AnContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task<AccountDto> GetAccountByEmail(string email)
        {
            var account = await _dbContext.Accounts
				.Where(a => a.Email.ToLower() == email.ToLower())
				.Select(a => new AccountDto
				{
					AccountId = a.AccountId,
					Email = a.Email,
					Password = a.Password,
					IsAdmin = a.IsAdmin,

				})
				.FirstOrDefaultAsync();	

            return account;
        }
        public async Task<List<Account>> GetAllAccounts()
		{
			var accounts = await _dbContext.Accounts
				.Include(x => x.Address)
				.ToListAsync();

			return accounts;

		}

        public async Task<bool> CreateAccount(Account newAccount)
        {
            try
            {
                await _dbContext.Accounts.AddAsync(newAccount);
                int result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
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
				.Include(x => x.CartItems)
				.ThenInclude(x => x.Product)
                .ThenInclude(product => product.ProductDetails)
				.ThenInclude(details => details.ProductInformation)
                    .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
				.ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (shoppingCart == null)
            {
                throw new Exception($"Shopping cart not found for account ID {accountId}");
            }

            return shoppingCart;
		}

		public async Task<ShoppingCart> AddProductToShoppingCart(AddProductToShoppingCartModel model)
		{
            var shoppingCart = await _dbContext.ShoppingCarts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.AccountId == model.AccountId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    AccountId = model.AccountId,
                    CartItems = new List<CartItem>()
                };

                _dbContext.ShoppingCarts.Add(shoppingCart);
                await _dbContext.SaveChangesAsync();
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == model.ProductId);
            if (product == null)
            {
                throw new Exception($"Product with ID {model.ProductId} not found.");
            }

            var existingCartItem = shoppingCart.CartItems.FirstOrDefault(item =>
                  item.ProductId == model.ProductId && item.Size == model.Size);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += model.Quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    Size = model.Size,
                    ShoppingCartId = shoppingCart.Id 
                };

                shoppingCart.CartItems.Add(newCartItem);
            }

            await _dbContext.SaveChangesAsync();

            return shoppingCart;


            //var cartItem = new CartItem
            //{
            //	ProductId = model.ProductId,
            //	Quantity = model.Quantity
            //};

            //if (shoppingCart != null)
            //{
            //	shoppingCart.CartItems.Add(cartItem);

            //	await _dbContext.SaveChangesAsync();
            //}


        }

		public Task UpdateAccount(Account accountId)
		{
			throw new NotImplementedException();
		}

		public async Task EmptyShoppingCart(int accountId)
		{
			var account = await _dbContext.Accounts
				.Include(x => x.ShoppingCart)
				.ThenInclude(x => x.CartItems)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			if (account != null)
			{
				account.ShoppingCart.CartItems.Clear();

				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task IncreaseShoppingCartProduct(int accountId, int productId)
		{
			var account = await _dbContext.Accounts
				.Include(x => x.ShoppingCart)
				.ThenInclude(x => x.CartItems)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId);

			if (product != null)
			{
				product.Quantity++; // Increase the quantity by 1
			}

			await _dbContext.SaveChangesAsync();

		}

		public async Task DecreaseShoppingCartProduct(int accountId, int productId)
		{
			var account = await _dbContext.Accounts
				.Include(x => x.ShoppingCart)
				.ThenInclude(x => x.CartItems)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId);

			if (product != null)
			{
				product.Quantity--; // decrease the quantity by 1
			}

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteCartItemFromShoppingCart(int accountId, int productId)
		{
			var account = await _dbContext.Accounts
				.Include(x => x.ShoppingCart)
				.ThenInclude(x => x.CartItems)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.AccountId == accountId);

			var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId);

			if (product != null)
			{
				account.ShoppingCart.CartItems.Remove(product);
			}

			await _dbContext.SaveChangesAsync();
		}
	}
}
