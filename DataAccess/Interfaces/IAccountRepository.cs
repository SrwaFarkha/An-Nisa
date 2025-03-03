using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.DatabaseEnums;
using DatabaseModels.Models;
using SharedModels.AccountModels;

namespace DataAccess.Interfaces
{
	public interface IAccountRepository
	{
		Task<AccountDto> GetAccountByEmail(string email);

        Task<List<Account>> GetAllAccounts();
		Task<bool> CreateAccount(Account newAccount);

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
		Task<bool> IncreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
		Task<bool> DecreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
		Task DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size);
	}
}
