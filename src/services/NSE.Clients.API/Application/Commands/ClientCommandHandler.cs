using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NSE.Clients.API.Application.Events;
using NSE.Clients.API.Models;
using NSE.Core.Messages;

namespace NSE.Clients.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler,
            IRequestHandler<CreateClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(CreateClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Client(message.Id, message.Name, message.Email, message.Cpf);

            var existedClient = await _clientRepository.GetByCpf(client.Cpf.Number);

            if(existedClient != null)
            {
                AddError("This Cpf is already in use.");
                return ValidationResult;
            }

            _clientRepository.Add(client);

            client.AddEvent(new ClientCreatedEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await SaveData(_clientRepository.UnitOfWork);
        }
    }
}
