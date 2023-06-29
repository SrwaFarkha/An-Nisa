using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AccountModels;

namespace BusinessLogic.Interfaces
{
	public interface IAccountService
	{
		Task<List<AccountDto>> GetAllAccounts();
		Task CreateAccount(CreateAccountModel model);
		Task UpdateAccount(int accountId, UpdateAccountModel model);
		Task<AccountDto> GetAccountById(int accountId);
		Task<List<AddressDto>> GetAddresses();
		Task<AddressDto> GetAddressById(int addressId);
		Task CreateAddress(CreateAddressModel model);
		Task DeleteAddress(int addressId);
		Task UpdateAddress(int addressId, UpdateAddressModel model);
		Task<ShoppingCartDto> GetShoppingCartByAccountId(int accountId);
		Task<ShoppingCartDto> AddProductToShoppingCart(AddProductToShoppingCartModel model);
		Task<string> EmptyShoppingCart(int accountId);
		Task<string> IncreaseShoppingCartProduct(int accountId, int productId);
		Task<string> DecreaseShoppingCartProduct(int accountId, int productId);
		Task<string> DeleteCartItemFromShoppingCart(int accountId, int productId);
	}
}
