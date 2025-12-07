using Fiap.FCG.Payment.Application.Pagamentos.Criar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.WebApi.Pagamentos.Criar
{
    [ApiController]
    [Route("api/pagamentos")]
    [ApiExplorerSettings(GroupName = "Pagamentos")]
    public class CriarPagamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CriarPagamentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria um pagamento",
            Description = "Inicia o processo de pagamento para um pedido informado."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarPagamentoCommand command)
        {
            var result = await _mediator.Send(command);

            var response = new
            {
                sucesso = result.Sucesso,
                mensagem = result.Sucesso ? "Pagamento iniciado com sucesso." : result.Erro,
                valor = result.Valor
            };

            return result.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
