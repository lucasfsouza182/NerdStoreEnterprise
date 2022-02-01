using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.Bff.Shop.Extensions;

namespace NSE.Bff.Shop.Services
{
    public interface ICartService
    {
    }

    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }
    }
}