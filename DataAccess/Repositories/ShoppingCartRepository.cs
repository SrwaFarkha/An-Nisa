using DataAccess.Interfaces;
using DatabaseModels.DatabaseEnums;
using DatabaseModels.DataContext;
using DatabaseModels.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AnContext _dbContext;

        public ShoppingCartRepository(AnContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<bool> IncreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size)
        {
            var account = await _dbContext.Accounts
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);

            var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
            if (product == null)
            {
                return false;
            }

            product.Quantity++;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DecreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size)
        {
            var account = await _dbContext.Accounts
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);

            var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
            if (product == null)
            {
                return false;
            }


            if (product.Quantity == 1)
            {
                account.ShoppingCart.CartItems.Remove(product);
            }
            else
            {
                product.Quantity--;

            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size)
        {
            var account = await _dbContext.Accounts
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);

            var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);

            if (product != null)
            {
                account.ShoppingCart.CartItems.Remove(product);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
