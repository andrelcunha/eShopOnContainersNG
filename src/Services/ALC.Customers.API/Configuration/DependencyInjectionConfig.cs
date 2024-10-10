using ALC.Core.Mediator;
using ALC.Customers.API.Application.Commands;
using ALC.Customers.API.Application.Events;
using ALC.Customers.API.Data;
using ALC.Customers.API.Data.Repository;
using ALC.Customers.API.Models;
using ALC.Customers.API.Services;
using FluentValidation.Results;
using MediatR;
using EasyNetQ;

namespace ALC.Customers.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();

        services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();

        services.AddHostedService<RegisterCustomerIntegrationHandler>();
        services.AddEasyNetQ("host=localhost:5672").UseSystemTextJson();

    }
}
