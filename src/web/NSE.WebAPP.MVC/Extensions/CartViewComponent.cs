using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Models;
using NSE.WebAPP.MVC.Services;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _cartService.GetCart() ?? new CartViewModel());
        }
    }
}