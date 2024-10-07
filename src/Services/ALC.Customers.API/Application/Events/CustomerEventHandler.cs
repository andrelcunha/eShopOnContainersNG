using ALC.Customers.API.Models;
using MediatR;

namespace ALC.Customers.API.Application.Events;

public class CustomerEventHandler : INotificationHandler<CustomerRegisteredEvent>
{
    public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
    {
        //send some notification email
        return Task.CompletedTask;
    }
}
