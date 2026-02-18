using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCG.Payment.Infrastructure.Messaging
{
    public static class ServiceBusDependencyInjection
    {
        public static IServiceCollection AddServiceBusMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new ServiceBusOptions
            {
                ConnectionString = configuration["SERVICEBUS_CONNECTION"] ?? "",
                QueueComprasRealizadas = configuration["SERVICEBUS_QUEUE"] ?? "compras-realizadas"
            };

            services.AddSingleton(options);

            services.AddSingleton(_ => new ServiceBusClient(options.ConnectionString));

            services.AddHostedService<ComprasRealizadasConsumer>();

            return services;
        }
    }

}
