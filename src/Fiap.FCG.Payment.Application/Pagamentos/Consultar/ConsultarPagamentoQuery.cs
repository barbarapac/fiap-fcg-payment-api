using Fiap.FCG.Payment.Domain._Shared;
using MediatR;

namespace Fiap.FCG.Payment.Application.Pagamentos.Consultar
{
    public class ConsultarPagamentoQuery : IRequest<Result<PagamentoDetalheResponse>>
    {
        public int PagamentoId { get; }

        public ConsultarPagamentoQuery(int pagamentoId)
        {
            PagamentoId = pagamentoId;
        }
    }
}
