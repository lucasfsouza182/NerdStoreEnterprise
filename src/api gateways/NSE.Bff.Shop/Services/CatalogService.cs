using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NSE.Bff.Shop.Extensions;

namespace NSE.Bff.Shop.Services
{
    public interface ICatalogService
    {
    }

    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }
    }
}