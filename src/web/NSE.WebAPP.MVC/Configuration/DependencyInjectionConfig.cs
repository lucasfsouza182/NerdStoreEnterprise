using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebAPP.MVC.Extensions;
using NSE.WebAPP.MVC.Services;
using NSE.WebAPP.MVC.Services.Handlers;

namespace NSE.WebAPP.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            //services.AddHttpClient<ICatalogService, CatalogService>()
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddHttpClient("Refit",
                    options =>
                    {
                        options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
                    })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

        }
    }
}
