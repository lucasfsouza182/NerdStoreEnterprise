using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Shop.Services;
using NSE.WebAPI.Core.Controllers;
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
            return CustomResponse();
        }

        [HttpGet]
        [Route("shop/cart-count")]
        public async Task<IActionResult> GetCartCount()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("shop/cart/items")]
        public async Task<IActionResult> AddCartItem()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("shop/cart/items/{produtoId}")]
        public async Task<IActionResult> UpdateCartItem()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("shop/cart/items/{produtoId}")]
        public async Task<IActionResult> RemoveCartItem()
        {
            return CustomResponse();
        }
    }
}
