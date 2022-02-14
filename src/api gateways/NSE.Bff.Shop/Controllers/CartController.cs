using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Shop.Models;
using NSE.Bff.Shop.Services;
using NSE.WebAPI.Core.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Bff.Shop.Controllers
{
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICartService cartService, ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("shop/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetCart());
        }

        [HttpGet]
        [Route("shop/cart-count")]
        public async Task<int> GetCartCount()
        {
            var qtd = await _cartService.GetCart();
            return qtd?.Items.Sum(i => i.Amount) ?? 0;
        }

        [HttpPost]
        [Route("shop/cart/items")]
        public async Task<IActionResult> AddCartItem(CartItemDTO productItem)
        {
            var product = await _catalogService.GetById(productItem.ProductId);

            await ValidateCartItem(product, productItem.Amount, true);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            productItem.Name = product.Name;
            productItem.Price = product.Price;
            productItem.Image = product.Image;

            var response = await _cartService.AddCartItem(productItem);

            return CustomResponse(response);
        }

        [HttpPut]
        [Route("shop/cart/items/{produtoId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItemDTO productItem)
        {
            var product = await _catalogService.GetById(productId);

            await ValidateCartItem(product, productItem.Amount);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            var response = await _cartService.UpdateCartItem(productId, productItem);
            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("shop/cart/items/{produtoId}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product == null)
            {
                AddError("Non-existent product!");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.RemoveCartItem(productId);

            return CustomResponse(response);
        }

        private async Task ValidateCartItem(ProductItemDTO product, int amount, bool addProduct = false)
        {
            if (product == null) AddError("Non-existent product!");
            if (amount < 1) AddError($"Choose at least one product unit {product.Name}");

            var cart = await _cartService.GetCart();
            var cartItem = cart.Items.FirstOrDefault(p => p.ProductId == product.Id);

            if (cartItem != null && addProduct && cartItem.Amount + amount > product.AmountStock)
            {
                AddError($"The product {product.Name} has {product.AmountStock} units in stock, you selected {amount}");
                return;
            }

            if (amount > product.AmountStock) AddError($"The product {product.Name} has {product.AmountStock} units in stock, you selected {amount}");
        }
    }
}
