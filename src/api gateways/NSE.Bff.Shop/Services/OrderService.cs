using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.Bff.Shop.Extensions;

namespace NSE.Bff.Shop.Services
{
    public interface IOrderService
    {
    }

    public class OrderService : Service, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
        }
    }
}