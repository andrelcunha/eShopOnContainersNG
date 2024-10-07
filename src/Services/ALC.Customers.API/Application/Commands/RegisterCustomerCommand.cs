using ALC.Core.Messages;
using FluentValidation;

namespace ALC.Customers.API.Application.Commands;

public class RegisterCustomerCommand : Command
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;

    public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterCustomerValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id must be informed");

            RuleFor(c => c.Cpf)
                .Must(HaveValidCpf)
                .WithMessage("The informed CPF is not valid");

            RuleFor(c => c.Email)
                .Must(HaveValidEmail)
                .WithMessage("The informed email is not valid");
        }

        private bool HaveValidCpf(string cpf)
        {
            return Core.DomainObjects.Cpf.IsValid(cpf);
        }

        private bool HaveValidEmail(string email)
        {
            return Core.DomainObjects.Email.IsValid(email);
        }
    }
}
