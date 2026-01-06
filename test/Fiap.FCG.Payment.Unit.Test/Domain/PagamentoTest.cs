using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.Domain.Pagamentos;
using Fiap.FCG.Payment.Unit.Test.Domain.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.Domain
{
    public class PagamentoTest : PagamentoFixture
    {
        [Fact]
        public void Criar_QuandoUsuarioInvalido_DeveRetornarErro()
        {
            // Arrange
            var usuarioId = 0;


            // Act
            var resultado = Pagamento.Criar(CompraId, usuarioId, Valor, EMetodoPagamento.Pix, null);


            // Assert
            resultado.Sucesso.Should().BeFalse();
            resultado.Erro.Should().Be("Usuário inválido.");
        }


        [Fact]
        public void Criar_QuandoValorInvalido_DeveRetornarErro()
        {
            // Arrange
            var valor = 0;


            // Act
            var resultado = Pagamento.Criar(CompraId, UsuarioId, valor, EMetodoPagamento.Pix, null);


            // Assert
            resultado.Sucesso.Should().BeFalse();
            resultado.Erro.Should().Be("Valor deve ser maior que zero.");
        }


        [Fact]
        public void Criar_QuandoCreditoSemBandeira_DeveRetornarErro()
        {
            // Arrange
            // Act
            var resultado = Pagamento.Criar(CompraId, UsuarioId, Valor, EMetodoPagamento.Credito, null);


            // Assert
            resultado.Sucesso.Should().BeFalse();
            resultado.Erro.Should().Be("Bandeira do cartão é obrigatória para crédito.");
        }


        [Fact]
        public void Criar_QuandoPagamentoViaPix_DeveAprovarAutomaticamente()
        {
            // Arrange
            // Act
            var resultado = Pagamento.Criar(CompraId, UsuarioId, Valor, EMetodoPagamento.Pix, null);


            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Status.Should().Be(EStatusPagamento.Aprovado);
        }


        [Fact]
        public void Criar_QuandoCreditoComBandeiraElo_DeveRecusar()
        {
            // Arrange
            var bandeira = EBandeiraCartao.Elo;


            // Act
            var resultado = Pagamento.Criar(CompraId, UsuarioId, Valor, EMetodoPagamento.Credito, bandeira);


            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Status.Should().Be(EStatusPagamento.Recusado);
        }


        [Theory]
        [InlineData(EBandeiraCartao.Visa)]
        [InlineData(EBandeiraCartao.Mastercard)]
        [InlineData(EBandeiraCartao.Amex)]
        public void Criar_QuandoCreditoComBandeirasValidas_DeveAprovar(EBandeiraCartao bandeira)
        {
            // Arrange


            // Act
            var resultado = Pagamento.Criar(CompraId, UsuarioId, Valor, EMetodoPagamento.Credito, bandeira);


            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Valor.Status.Should().Be(EStatusPagamento.Aprovado);
        }
    }
}
