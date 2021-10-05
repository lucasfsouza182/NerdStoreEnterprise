using Microsoft.Extensions.DependencyInjection;
using NSE.WebAPP.MVC.Services;

namespace NSE.WebAPP.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        }
    }
}
