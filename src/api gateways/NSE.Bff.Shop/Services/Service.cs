﻿using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.Bff.Shop.Services
{
    public abstract class Service
    {
        protected StringContent GetContent(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}