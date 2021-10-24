using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customers.API.Application.Commands;

namespace NSE.Customers.API.Services
{
    public class CreateCustomerIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");
            _bus.RespondAsync<UserCreatedIntegrationEvent, ResponseMessage>(async request =>
                new ResponseMessage(await CreateCustomer(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> CreateCustomer(UserCreatedIntegrationEvent message)
        {
            var customerCommand = new CreateCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;

            // service
            // Não pode injetar scope em seviços singleton
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(customerCommand);
            }

            return success;
        }
    }
}
