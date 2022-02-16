using NSE.Core.Communication;
using NSE.WebAPP.MVC.Models;
using System;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Services
{
    public interface IShopBffService
    {
        Task<CartViewModel> GetCart();
        Task<int> GetCountCart();
        Task<ResponseResult> AddCartItem(CartItemViewModel product);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel product);
        Task<ResponseResult> RemoveCartItem(Guid productId);
    }
}
