using Fiap.FCG.Payment.Application.Pagamentos.Consultar;
using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.WebApi.Pagamentos.Consultar;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.WebApi.Consultar
{
    public class ConsultarPagamentoControllerTest
    {
        [Fact]
        public async Task Consultar_QuandoPagamentoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<ConsultarPagamentoQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<PagamentoDetalheResponse>("Pagamento não encontrado"));


            var controller = new ConsultarPagamentoDetalheController(mediator.Object);


            // Act
            var result = await controller.Consultar(99);


            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFound = result as NotFoundObjectResult;
            notFound.Value.Should().BeEquivalentTo(new
            {
                sucesso = false,
                mensagem = "Pagamento não encontrado"
            });
        }


        [Fact]
        public async Task Consultar_QuandoPagamentoExiste_DeveRetornarOk()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var pagamento = new PagamentoDetalheResponse
            {
                Id = 10,
                UsuarioId = 1,
                Valor = 100,
                Status = "Aprovado",
                CriadoEm = System.DateTime.UtcNow
            };


            mediator.Setup(m => m.Send(It.IsAny<ConsultarPagamentoQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(pagamento));


            var controller = new ConsultarPagamentoDetalheController(mediator.Object);


            // Act
            var result = await controller.Consultar(10);


            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok.Value.Should().BeEquivalentTo(new
            {
                sucesso = true,
                pagamento
            });
        }
    }
}
