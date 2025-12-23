using Fiap.FCG.Payment.Application.Pagamentos.Consultar;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.WebApi.Pagamentos.Consultar
{
    [ApiController]
    [Route("api/pagamentos")]
    [ApiExplorerSettings(GroupName = "Pagamentos")]
    public class ConsultarPagamentoDetalheController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultarPagamentoDetalheController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{pagamentoId}")]
        [SwaggerOperation(
            Summary = "Consulta detalhes de um pagamento",
            Description = "Retorna informações do pagamento, incluindo transações e status."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Consultar(int pagamentoId)
        {
            var result = await _mediator.Send(new ConsultarPagamentoQuery(pagamentoId));

            if (!result.Sucesso)
            {
                return NotFound(new
                {
                    sucesso = false,
                    mensagem = result.Erro
                });
            }

            return Ok(new
            {
                sucesso = true,
                pagamento = result.Valor
            });
        }
    }
}
