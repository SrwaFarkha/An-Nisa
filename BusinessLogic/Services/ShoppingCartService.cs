using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DatabaseModels.DatabaseEnums;
using SharedModels.AccountModels;
using SharedModels.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IAccountRepository _accountRepository;


        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IAccountRepository accountRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _accountRepository = accountRepository;

        }

        public async Task<ShoppingCartDto> GetShoppingCartByAccountId(int accountId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByAccountId(accountId);

            var shoppingCartDto = new ShoppingCartDto
            {
                Products = shoppingCart.CartItems.Select(item => new CartItemDto
                {
                    ProductId = item.Product.ProductId,
                    ProductName = item.Product.ProductName,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    Size = item.Size,
                    Color = item.Product.ProductDetails?.ProductInformation?.Color,
                    ImageUrl = item.Product.Images.FirstOrDefault()?.Url,

                    TotalProductPrice = item.Quantity * item.Product.Price
                }).ToList(),
                ShoppingCartTotalPrice = shoppingCart.CartItems.Sum(item => item.Quantity * item.Product.Price)
            };

            return shoppingCartDto;

        }

        public async Task<ShoppingCartDto> AddProductToShoppingCart(AddProductToShoppingCartModel model)
        {
            await _shoppingCartRepository.AddProductToShoppingCart(model);

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByAccountId(model.AccountId);

            var shoppingCartDto = new ShoppingCartDto
            {
                Products = shoppingCart.CartItems.Select(item => new CartItemDto
                {
                    ProductId = item.Product.ProductId,
                    ProductName = item.Product.ProductName,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    Size = item.Size,
                    TotalProductPrice = item.Quantity * item.Product.Price
                }).ToList(),
                ShoppingCartTotalPrice = shoppingCart.CartItems.Sum(item => item.Quantity * item.Product.Price)
            };

            return shoppingCartDto;
        }

        public async Task<string> EmptyShoppingCart(int accountId)
        {
            await _shoppingCartRepository.EmptyShoppingCart(accountId);

            return "ShoppingCart emptied";
        }

        public async Task<bool> IncreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size)
        {
            var result = await _shoppingCartRepository.IncreaseShoppingCartProduct(accountId, productId, size);
            return result;

        }

        public async Task<bool> DecreaseShoppingCartProduct(int accountId, int productId, DatabaseEnums.Size size)
        {
            var result = await _shoppingCartRepository.DecreaseShoppingCartProduct(accountId, productId, size);
            return result;
        }

        public async Task<string> DeleteCartItemFromShoppingCart(int accountId, int productId, DatabaseEnums.Size size)
        {
            await _shoppingCartRepository.DeleteCartItemFromShoppingCart(accountId, productId, size);

            var account = await _accountRepository.GetAccountById(accountId);
            if (account != null)
            {
                var product = account.ShoppingCart.CartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
                if (product != null)
                {
                    account.ShoppingCart.CartItems.Remove(product);
                }
            }
            return "Succesfull";


        }
    }
}
