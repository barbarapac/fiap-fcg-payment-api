using Azure.Messaging.ServiceBus;
using Moq;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.Unit.Test.Infrastructure.Mocks
{
    public class ServiceBusSenderMock : Mock<ServiceBusSender>
    {
        public void GarantirEnvioMensagem() =>
        Verify(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default), Times.Once);


        public void ConfigurarEnvioMensagem() =>
        Setup(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default))
        .Returns(Task.CompletedTask);
    }
}
