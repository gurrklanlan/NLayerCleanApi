using App.Application.Contracts.ServiceBus;
using App.Domain.Const;
using App.Domain.Options;
using CleanApp.Api.Consumer;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AddBus
{
    public static class BusExtensionMethod
    {
        public static void AddBussExt(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddScoped<IServiceBus, ServiceBus>();

            services.AddMassTransit(x =>
            {

                x.AddConsumer<ProductAddedEventConsumer>();



                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url)
                    {
                       
                    });

                    cfg.ReceiveEndpoint(ServiceBusConst.ProductAddedEventQueueName,
                        e =>
                    {
                        e.ConfigureConsumer<ProductAddedEventConsumer>(context);
                    });
                });
            });
        }
    }
}
