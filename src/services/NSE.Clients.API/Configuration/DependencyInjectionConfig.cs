using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSE.Clients.API.Application.Commands;
using NSE.Core.Mediator;

namespace NSE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateClientCommand, ValidationResult>, ClientCommandHandler>();
        }
    }
}
