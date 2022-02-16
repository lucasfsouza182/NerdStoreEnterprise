using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Models;
using NSE.WebAPP.MVC.Services;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IShopBffService _shopBffService;

        public CartViewComponent(IShopBffService shopBffService)
        {
            _shopBffService = shopBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _shopBffService.GetCountCart());
        }
    }
}