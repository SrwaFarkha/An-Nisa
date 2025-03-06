using DatabaseModels.DatabaseEnums;
using DatabaseModels.Models;
using SharedModels.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByAccountId(int accountId);
        Task<ShoppingCart> AddProductToShoppingCart(AddProductToShoppingCartModel model);
        Task EmptyShoppingCart(int accountId);
        Task<bool> IncreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
        Task<bool> DecreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size);
        Task DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size);
    }
}
