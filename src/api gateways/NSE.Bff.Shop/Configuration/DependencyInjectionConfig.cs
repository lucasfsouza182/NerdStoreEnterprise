using Microsoft.Extensions.DependencyInjection;
using NSE.WebAPI.Core.User;
using Microsoft.AspNetCore.Http;

namespace NSE.Bff.Shop.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
        }
    }
}
