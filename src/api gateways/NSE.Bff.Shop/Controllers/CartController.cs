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
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, ICatalogService catalogService, IOrderService orderService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
            _orderService = orderService;
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
            return qtd?.Itens.Sum(i => i.Amount) ?? 0;
        }

        [HttpPost]
        [Route("shop/cart/items")]
        public async Task<IActionResult> AddCartItem(CartItemDTO productItem)
        {
            var product = await _catalogService.GetById(productItem.ProductId);

            await ValidateCartItem(product, productItem.Amount, true);
            if (!ValidOperation()) return CustomResponse();

            productItem.Name = product.Name;
            productItem.Price = product.Price;
            productItem.Image = product.Image;

            var response = await _cartService.AddCartItem(productItem);

            return CustomResponse(response);
        }

        [HttpPut]
        [Route("shop/cart/items/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItemDTO productItem)
        {
            var product = await _catalogService.GetById(productId);

            await ValidateCartItem(product, productItem.Amount);
            if (!ValidOperation()) return CustomResponse();

            var response = await _cartService.UpdateCartItem(productId, productItem);
            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("shop/cart/items/{productId}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product == null)
            {
                AddError("Non-existent product!");
                if (!ValidOperation()) return CustomResponse();
            }

            var response = await _cartService.RemoveCartItem(productId);

            return CustomResponse(response);
        }

        [HttpPost]
        [Route("shop/cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher([FromBody] string voucherCode)
        {
            var voucher = await _orderService.GetVoucherByCode(voucherCode);
            if (voucher is null)
            {
                AddError("Voucher invalid ou not found!");
                return CustomResponse();
            }

            var response = await _cartService.ApplyVoucherCart(voucher);

            return CustomResponse(response);
        }

        private async Task ValidateCartItem(ProductItemDTO product, int amount, bool addProduct = false)
        {
            if (product == null) AddError("Non-existent product!");
            if (amount < 1) AddError($"Choose at least one product unit {product.Name}");

            var cart = await _cartService.GetCart();
            var cartItem = cart.Itens.FirstOrDefault(p => p.ProductId == product.Id);

            if (cartItem != null && addProduct && cartItem.Amount + amount > product.QuantityStock)
            {
                AddError($"The product {product.Name} has {product.QuantityStock} units in stock, you selected {amount}");
                return;
            }

            if (amount > product.QuantityStock) AddError($"The product {product.Name} has {product.QuantityStock} units in stock, you selected {amount}");
        }
    }
}
