using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Bff.Shop.Extensions;
using NSE.Bff.Shop.Models;
using NSE.Core.Communication;

namespace NSE.Bff.Shop.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCart();
        Task<ResponseResult> AddCartItem(CartItemDTO product);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemDTO carrinho);
        Task<ResponseResult> RemoveCartItem(Guid productId);
        Task<ResponseResult> ApplyVoucherCart(VoucherDTO voucher);
    }

    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartDTO> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleResponseErrors(response);

            return await DeserializeResponseObject<CartDTO>(response);
        }

        public async Task<ResponseResult> AddCartItem(CartItemDTO product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemDTO product)
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

        public async Task<ResponseResult> ApplyVoucherCart(VoucherDTO voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("/cart/apply-voucher/", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }
    }
}