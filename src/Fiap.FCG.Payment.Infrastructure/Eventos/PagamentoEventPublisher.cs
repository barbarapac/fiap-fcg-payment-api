using Azure.Messaging.ServiceBus;
using Fiap.FCG.Payment.Domain.Eventos;
using Fiap.FCG.Payment.Domain.Pagamentos;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.Infrastructure.Eventos
{
    public class PagamentoEventPublisher : IPagamentoEventPublisher
    {
        private readonly ServiceBusSender _sender;

        public PagamentoEventPublisher(IConfiguration config)
        {
            var conn = config["SERVICEBUS_CONNECTION"];
            var client = new ServiceBusClient(conn);

            _sender = client.CreateSender("payment-events");
        }

        public async Task PublicarPagamentoCriadoAsync(Pagamento pagamento)
        {
            var payload = JsonSerializer.Serialize(new
            {
                pagamento.Id,
                pagamento.UsuarioId,
                pagamento.JogoId,
                pagamento.Valor
            });

            await _sender.SendMessageAsync(new ServiceBusMessage(payload));
        }
    }
}
