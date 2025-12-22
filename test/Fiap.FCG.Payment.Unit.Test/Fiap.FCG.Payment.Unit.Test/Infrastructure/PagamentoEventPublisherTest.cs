using Fiap.FCG.Payment.Infrastructure.Eventos;
using Fiap.FCG.Payment.Unit.Test.Infrastructure.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.Infrastructure
{
    public class PagamentoEventPublisherTest
    {
        [Fact]
        public async Task PublicarPagamentoCriadoAsync_DeveEnviarMensagemParaServiceBus()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["SERVICEBUS_CONNECTION"]).Returns("Endpoint=sb://fake/;SharedAccessKeyName=key;SharedAccessKey=value");


            var pagamento = Fakers.PagamentoFaker.Valido();


            // Act
            var publisher = new PagamentoEventPublisherTestable(configMock.Object);
            await publisher.PublicarPagamentoCriadoAsync(pagamento);


            // Assert
            publisher.SenderMock.GarantirEnvioMensagem();
        }


        private class PagamentoEventPublisherTestable : PagamentoEventPublisher
        {
            public ServiceBusSenderMock SenderMock { get; }


            public PagamentoEventPublisherTestable(IConfiguration config) : base(config)
            {
                SenderMock = new ServiceBusSenderMock();
                typeof(PagamentoEventPublisher)
                .GetField("_sender", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(this, SenderMock.Object);


                SenderMock.ConfigurarEnvioMensagem();
            }
        }
    }
}
