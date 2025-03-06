using DatabaseModels.DatabaseEnums;
using SharedModels.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCartByAccountId(int accountId);
        Task<ShoppingCartDto> AddProductToShoppingCart(AddProductToShoppingCartModel model);
        Task<string> EmptyShoppingCart(int accountId);
        Task<bool> IncreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
        Task<bool> DecreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
        Task<string> DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size);
    }
}
