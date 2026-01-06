using Fiap.FCG.Payment.Application.Pagamentos.Criar;
using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.WebApi.Pagamentos.Criar;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.WebApi.Criar
{
    public class CriarPagamentoControllerTest
    {
        [Fact]
        public async Task Criar_QuandoPagamentoValido_DeveRetornarOk()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var command = new CriarPagamentoCommand
            {
                CompraId = 1,
                UsuarioId = 1,
                ValorTotal = 100,
                MetodoPagamento = EMetodoPagamento.Pix
            };


            mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(123));


            var controller = new CriarPagamentoController(mediator.Object);


            // Act
            var result = await controller.Criar(command);


            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new
            {
                sucesso = true,
                mensagem = "Pagamento iniciado com sucesso.",
                valor = 123
            });
        }


        [Fact]
        public async Task Criar_QuandoPagamentoInvalido_DeveRetornarBadRequest()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var command = new CriarPagamentoCommand();


            mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int>("Erro ao criar pagamento"));


            var controller = new CriarPagamentoController(mediator.Object);


            // Act
            var result = await controller.Criar(command);


            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var bad = result as BadRequestObjectResult;
            bad.Value.Should().BeEquivalentTo(new
            {
                sucesso = false,
                mensagem = "Erro ao criar pagamento",
                valor = 0
            });
        }
    }
}
