using Fiap.FCG.Payment.Application.Pagamentos.Consultar;
using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.Domain.Pagamentos;
using Fiap.FCG.Payment.Unit.Test.Application.Mocks;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.Application
{
    public class ConsultarPagamentoHandlerTest
    {
        [Fact]
        public async Task Handle_QuandoPagamentoNaoExiste_DeveRetornarErro()
        {
            // Arrange
            var repositoryMock = new IPagamentoRepositoryMock();
            repositoryMock.ConfigurarObterPorIdAsync(null);
            var handler = new ConsultarPagamentoHandler(repositoryMock.Object);
            var query = new ConsultarPagamentoQuery(123);


            // Act
            var result = await handler.Handle(query, CancellationToken.None);


            // Assert
            result.Sucesso.Should().BeFalse();
            result.Erro.Should().Be("Pagamento não encontrado.");
        }


        [Fact]
        public async Task Handle_QuandoPagamentoExiste_DeveRetornarDetalhe()
        {
            // Arrange
            var pagamento = Pagamento.Criar(1, 2, 100, EMetodoPagamento.Pix, null).Valor;
            var repositoryMock = new IPagamentoRepositoryMock();
            repositoryMock.ConfigurarObterPorIdAsync(pagamento);
            var handler = new ConsultarPagamentoHandler(repositoryMock.Object);
            var query = new ConsultarPagamentoQuery(pagamento.Id);


            // Act
            var result = await handler.Handle(query, CancellationToken.None);


            // Assert
            result.Sucesso.Should().BeTrue();
            result.Valor.Should().NotBeNull();
            result.Valor.Id.Should().Be(pagamento.Id);
            result.Valor.UsuarioId.Should().Be(pagamento.UsuarioId);
        }
    }
}
