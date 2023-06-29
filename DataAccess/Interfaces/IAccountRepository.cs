using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Models.AccountModels;

namespace DataAccess.Interfaces
{
	public interface IAccountRepository
	{
		Task<List<Account>> GetAllAccounts();
		Task CreateAccount(Account newAccount);
		Task UpdateAccount(int accountId, UpdateAccountModel model);
		Task<Account> GetAccountById(int accountId);
		Task<List<Address>> GetAddresses();
		Task<Address> GetAddressById(int addressId);
		Task CreateAddress(Address newAddress);
		Task DeleteAccount(int addressId);
		Task UpdateAddress(int addressId, UpdateAddressModel model);
		Task<ShoppingCart> GetShoppingCartByAccountId(int accountId);
		Task<ShoppingCart> AddProductToShoppingCart(AddProductToShoppingCartModel model);
		Task UpdateAccount(Account accountId);
		Task EmptyShoppingCart(int accountId);
		Task IncreaseShoppingCartProduct(int accountId, int productId);
		Task DecreaseShoppingCartProduct(int accountId, int productId);
		Task DeleteCartItemFromShoppingCart(int accountId, int productId);
	}
}
