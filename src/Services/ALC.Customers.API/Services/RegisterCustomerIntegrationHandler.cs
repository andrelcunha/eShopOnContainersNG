using ALC.Core.Mediator;
using ALC.Core.Messages.Integration;
using ALC.Customers.API.Application.Commands;
using EasyNetQ;
using FluentValidation.Results;

namespace ALC.Customers.API.Services;

public class RegisterCustomerIntegrationHandler : BackgroundService
{
    private IBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider, IBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // _bus = RabbitHutch.CreateBus("host=localhost:5672");

        _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request => 
            new ResponseMessage(await RegisterCustomer(request)));

        return Task.CompletedTask;
    }
    private async Task<ValidationResult> RegisterCustomer(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new RegisterCustomerCommand(
            message.Id,
            message.Name,
            message.Email,
            message.Cpf);

        ValidationResult success;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            success = await mediator.SendCommand(customerCommand);
        }

        return success;
    }
}
