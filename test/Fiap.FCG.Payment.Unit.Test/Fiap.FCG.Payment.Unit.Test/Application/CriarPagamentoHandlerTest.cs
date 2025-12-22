using Fiap.FCG.Payment.Application.Pagamentos.Criar;
using Fiap.FCG.Payment.Unit.Test.Application.Fakers;
using Fiap.FCG.Payment.Unit.Test.Application.Mocks;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.Application
{
    public class CriarPagamentoHandlerTest
    {
        [Fact]
        public async Task Handle_QuandoCriacaoInvalida_DeveRetornarErro()
        {
            // Arrange
            var repositoryMock = new IPagamentoRepositoryMock();
            var handler = new CriarPagamentoHandler(repositoryMock.Object);
            var command = PagamentoCommandFaker.Invalido();


            // Act
            var result = await handler.Handle(command, CancellationToken.None);


            // Assert
            result.Sucesso.Should().BeFalse();
            result.Erro.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Handle_QuandoCriacaoValida_DevePersistirEPagar()
        {
            // Arrange
            var repositoryMock = new IPagamentoRepositoryMock();
            repositoryMock.ConfigurarAdicionarAsyncComId(123);
            var handler = new CriarPagamentoHandler(repositoryMock.Object);
            var command = PagamentoCommandFaker.Valido();


            // Act
            var result = await handler.Handle(command, CancellationToken.None);


            // Assert
            result.Sucesso.Should().BeTrue();
            result.Valor.Should().BeGreaterThan(0);
            repositoryMock.GarantirAdicionarAsync();
        }
    }
}
