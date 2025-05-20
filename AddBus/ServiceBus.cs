using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Contracts.ServiceBus;
using App.Domain.Events;
using MassTransit;
using MassTransit.Testing;

namespace AddBus
{
    public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
    {
        public async Task PublishAsync<T>(T @event, CancellationToken cancellation = default) where T : IEventOrMessage
        {
           await publishEndpoint.Publish(@event,cancellation);
        }

        public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellation = default) where T : IEventOrMessage
        {
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await sendEndpoint.Send(message, cancellation);
        }
    }
}
