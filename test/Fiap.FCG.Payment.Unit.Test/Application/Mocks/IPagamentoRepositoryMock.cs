using Fiap.FCG.Payment.Domain.Pagamentos;
using Moq;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.Unit.Test.Application.Mocks
{
    public class IPagamentoRepositoryMock : Mock<IPagamentoRepository>
    {
        public void ConfigurarAdicionarAsyncComId(int idGerado = 1) =>
            Setup(r => r.AdicionarAsync(It.IsAny<Pagamento>()))
                .Callback<Pagamento>(pagamento =>
                {
                    typeof(Pagamento)
                        .GetProperty("Id")!
                        .SetValue(pagamento, idGerado);
                })
                .Returns(Task.CompletedTask);

        public void ConfigurarObterPorIdAsync(Pagamento? pagamento) =>
            Setup(r => r.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pagamento);

        public void GarantirAdicionarAsync() =>
            Verify(r => r.AdicionarAsync(It.IsAny<Pagamento>()), Times.Once);
    }
}
