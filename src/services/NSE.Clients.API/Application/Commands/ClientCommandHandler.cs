using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NSE.Clients.API.Models;
using NSE.Core.Messages;

namespace NSE.Clients.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<CreateClientCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(CreateClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Client(message.Id, message.Name, message.Email, message.Cpf);

            // Validate

            // Save in database

            if(true)
            {
                AddError("This Cpf is already in use.");
            }

            return ValidationResult;
        }
    }
}
