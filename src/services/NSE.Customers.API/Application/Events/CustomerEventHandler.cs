using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NSE.Customers.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        public CustomerEventHandler()
        {
        }

        public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            // send confirmation event
            return Task.CompletedTask;
        }
    }
}
