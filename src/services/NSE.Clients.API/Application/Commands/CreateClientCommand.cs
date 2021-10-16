using System;
using FluentValidation;
using NSE.Core.Messages;

namespace NSE.Clients.API.Application.Commands
{
    public class CreateClientCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public CreateClientCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateClientValidation : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id invalid");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("The customer's name was not valid");

            RuleFor(c => c.Cpf)
                .Must(HasValidCpf)
                .WithMessage("The CPF is not valid.");

            RuleFor(c => c.Email)
                .Must(HasValidEmail)
                .WithMessage("The e-mail is not valid.");
            
        }

        protected static bool HasValidCpf(string cpf)
        {
            return Core.DomainObjects.Cpf.Validate(cpf);
        }

        protected static bool HasValidEmail(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }
    }
}
