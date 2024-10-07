using ALC.Core.Messages;
using ALC.Customers.API.Models;
using FluentValidation.Results;
using MediatR;

namespace ALC.Customers.API.Application.Commands;

public class CustomerCommandHandler : CommandHandler,
    IRequestHandler<RegisterCustomerCommand, ValidationResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ValidationResult> Handle(RegisterCustomerCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid())
            return message.ValidationResult;

        var customer = new Customer(message.Id, message.Name, message.Email, message.Cpf);

        var customerExists = await _customerRepository.GetByCpf(message.Cpf);

        if (customerExists is not null)
        {
            AddError("The informed CPF is already registered");
            return ValidationResult;
        }

        _customerRepository.Add(customer);

        customer.AddEvent(new CustomerRegistradedEvent(message.Id, message.Name, message.Email, message.Cpf));

        return await PersistData(_customerRepository.UnitOfWork);
    }
}
