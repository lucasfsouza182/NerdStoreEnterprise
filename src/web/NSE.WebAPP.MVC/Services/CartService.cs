using Microsoft.Extensions.Options;
using NSE.Core.Communication;
using NSE.WebAPP.MVC.Extensions;
using NSE.WebAPP.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebAPP.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleResponseErrors(response);

            return await DeserializeResponseObject<CartViewModel>(response);
        }

        public async Task<ResponseResult> AddCartItem(ProductItemViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, ProductItemViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PutAsync($"/cart/{product.ProductId}", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
