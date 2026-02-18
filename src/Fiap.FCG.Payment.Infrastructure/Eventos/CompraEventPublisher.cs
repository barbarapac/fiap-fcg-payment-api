using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.Infrastructure.Eventos
{
    public class CompraEventPublisher
    {
        private readonly ServiceBusClient _client;
        private readonly string _queueName;

        public CompraEventPublisher(ServiceBusClient client, IOptions<ServiceBusOptions> options)
        {
            _client = client;
            _queueName = options.Value.QueueComprasRealizadas;
        }

        public async Task PublicarCompraRealizadaAsync(object evento, CancellationToken ct = default)
        {
            var sender = _client.CreateSender(_queueName);

            var json = JsonSerializer.Serialize(evento);

            var message = new ServiceBusMessage(json)
            {
                ContentType = "application/json",
                Subject = "CompraRealizada"                
            };

            await sender.SendMessageAsync(message, ct);
        }
    }

    public class ServiceBusOptions
    {
        public string QueueComprasRealizadas { get; set; } = "compras-realizadas";
    }
}
