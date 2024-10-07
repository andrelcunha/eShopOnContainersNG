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

    public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return request.ValidationResult;

        var customer = new Customer(request.Id, request.Name, request.Email, request.Cpf);

        var customerExists = await _customerRepository.GetByCpf(request.Cpf);

        if (customerExists is not null)
        {
            AddError("The informed CPF is already registered");
            return ValidationResult;
        }

        _customerRepository.Add(customer);

        return await PersistData(_customerRepository.UnitOfWork);
    }
}
