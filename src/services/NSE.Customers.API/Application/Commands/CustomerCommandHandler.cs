using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NSE.Customers.API.Application.Events;
using NSE.Customers.API.Models;
using NSE.Core.Messages;

namespace NSE.Customers.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler,
            IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id, message.Name, message.Email, message.Cpf);

            var existedCustomer = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if(existedCustomer != null)
            {
                AddError("This Cpf is already in use.");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new CustomerCreatedEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await SaveData(_customerRepository.UnitOfWork);
        }
    }
}
