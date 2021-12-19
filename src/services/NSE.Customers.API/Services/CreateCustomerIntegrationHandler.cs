using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customers.API.Application.Commands;
using NSE.MessageBus;

namespace NSE.Customers.API.Services
{
    public class CreateCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<UserCreatedIntegrationEvent, ResponseMessage>(async request =>
                await CreateCustomer(request));

            _bus.AdvancedBus.Connected += OnConnect;

            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UserCreatedIntegrationEvent, ResponseMessage>(async request =>
               await CreateCustomer(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        private async Task<ResponseMessage> CreateCustomer(UserCreatedIntegrationEvent message)
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

            return new ResponseMessage(success);
        }
    }
}
