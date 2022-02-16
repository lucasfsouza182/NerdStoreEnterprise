using Microsoft.Extensions.Options;
using NSE.Core.Communication;
using NSE.WebAPP.MVC.Extensions;
using NSE.WebAPP.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Services
{
    public class ShopBffService : Service, IShopBffService
    {
        private readonly HttpClient _httpClient;

        public ShopBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ShopBffUrl);
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/shop/cart/");

            HandleResponseErrors(response);

            return await DeserializeResponseObject<CartViewModel>(response);
        }

        public async Task<int> GetCountCart()
        {
            var response = await _httpClient.GetAsync("shop/cart-count");

            HandleResponseErrors(response);

            return await DeserializeResponseObject<int>(response);
        }

        public async Task<ResponseResult> AddCartItem(CartItemViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/shop/cart/items", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PutAsync($"/shop/cart/items/{product.ProductId}", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/shop/cart/items/{productId}");

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
