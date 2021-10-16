using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NSE.Clients.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<ClientCreatedEvent>
    {
        public ClientEventHandler()
        {
        }

        public Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
        {
            // send confirmation event
            return Task.CompletedTask;
        }
    }
}
