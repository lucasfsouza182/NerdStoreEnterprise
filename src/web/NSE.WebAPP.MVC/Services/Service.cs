using System;
using System.Net.Http;
using NSE.WebAPP.MVC.Extensions;

namespace NSE.WebAPP.MVC.Services
{
    public class Service
    {
        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
