using NSE.WebAPP.MVC.Models;
using System;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCart();
        Task<ResponseResult> AddCartItem(ProductItemViewModel product);
        Task<ResponseResult> UpdateCartItem(Guid productId, ProductItemViewModel product);
        Task<ResponseResult> RemoveCartItem(Guid productId);
    }
}
