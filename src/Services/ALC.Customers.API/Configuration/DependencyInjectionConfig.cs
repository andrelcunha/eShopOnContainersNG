using ALC.Core.Mediator;
using ALC.Customers.API.Application.Commands;
using ALC.Customers.API.Data;
using ALC.Customers.API.Data.Repository;
using ALC.Customers.API.Models;
using FluentValidation.Results;
using MediatR;

namespace ALC.Customers.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
    }
}
