using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Cart.API.Data;
using NSE.Cart.API.Model;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;
using System;
using System.Threading.Tasks;

namespace NSE.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("cart")]
        public async Task<CartCustomer> GetCart()
        {
            return null;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItem(CartItem item)
        {
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateItem(Guid productId, CartItem item)
        {
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveItem(Guid productId)
        {
            return CustomResponse();
        }
    }
}
