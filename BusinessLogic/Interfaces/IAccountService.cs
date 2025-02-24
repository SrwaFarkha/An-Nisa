using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.DatabaseEnums;
using SharedModels.AccountModels;

namespace BusinessLogic.Interfaces
{
	public interface IAccountService
	{

        Task<List<AccountDto>> GetAllAccounts();
        Task<bool> CreateAccount(CreateAccountModel model);
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
		Task<string> IncreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
		Task<string> DecreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
		Task<string> DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size);
	}
}
