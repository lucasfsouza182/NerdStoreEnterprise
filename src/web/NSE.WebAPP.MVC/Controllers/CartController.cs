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
        private readonly IShopBffService _shopBffService;

        public CartController(IShopBffService shopBffService)
        {
            _shopBffService = shopBffService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _shopBffService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddCartItem(CartItemViewModel productItem)
        {
            var response = await _shopBffService.AddCartItem(productItem);

            if (ResponseHasErrors(response)) return View("Index", await _shopBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int amount)
        {
            var productItem = new CartItemViewModel { ProductId = productId, Amount = amount };
            var response = await _shopBffService.UpdateCartItem(productId, productItem);

            if (ResponseHasErrors(response)) return View("Index", await _shopBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var response = await _shopBffService.RemoveCartItem(productId);

            if (ResponseHasErrors(response)) return View("Index", await _shopBffService.GetCart());

            return RedirectToAction("Index");
        }
    }
}