﻿using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSE.Customers.API.Application.Commands;
using NSE.Customers.API.Application.Events;
using NSE.Customers.API.Data;
using NSE.Customers.API.Data.Repository;
using NSE.Customers.API.Models;
using NSE.Core.Mediator;
using NSE.Customers.API.Services;

namespace NSE.Customers.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerEventHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();
        }
    }
}
