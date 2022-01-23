using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return await GetCartCustomer() ?? new CartCustomer();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItem(CartItem item)
        {
            var cart = await GetCartCustomer();

            if (cart == null)
                HandleNewCart(item);
            else
                HandleExistedCart(cart, item);

            if (!ValidOperation()) return CustomResponse();

            await SaveData();
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

        private async Task<CartCustomer> GetCartCustomer()
        {
            return await _context.CartCustomer
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
        }

        private async Task SaveData()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddError("Unable to persist data in the database");
        }

        private void HandleNewCart(CartItem item)
        {
            var cart = new CartCustomer(_user.GetUserId());
            cart.AddItem(item);

            _context.CartCustomer.Add(cart);
        }

        private void HandleExistedCart(CartCustomer cart, CartItem item)
        {
            var existedProductItem = cart.CartExistedItem(item);

            cart.AddItem(item);            

            if (existedProductItem)
            {
                _context.CartItems.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _context.CartItems.Add(item);
            }

            _context.CartCustomer.Update(cart);
        }
    }
}
