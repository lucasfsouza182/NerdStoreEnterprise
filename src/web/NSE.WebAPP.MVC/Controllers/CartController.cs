using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Models;
using NSE.WebAPP.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICartService cartService,
                              ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddCartItem(ProductItemViewModel productItem)
        {
            var product = await _catalogService.GetById(productItem.ProductId);

            ValidateCartItem(product, productItem.Amount);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            productItem.Name = product.Name;
            productItem.Price = product.Price;
            productItem.Image = product.Image;

            var response = await _cartService.AddCartItem(productItem);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int amount)
        {
            var product = await _catalogService.GetById(productId);

            ValidateCartItem(product, amount);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            var productItem = new ProductItemViewModel { ProductId = productId, Amount = amount };
            var response = await _cartService.UpdateCartItem(productId, productItem);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product == null)
            {
                AddValidationError("Non-existent product!");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.RemoveCartItem(productId);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        private void ValidateCartItem(ProductViewModel product, int amount)
        {
            if (product == null) AddValidationError("Non-existent product!");
            if (amount < 1) AddValidationError($"Choose at least one product unit {product.Name}");
            if (amount > product.QuantityStock) AddValidationError($"The product {product.Name} has {product.QuantityStock} units in stock, you selected {amount}");
        }
    }
}