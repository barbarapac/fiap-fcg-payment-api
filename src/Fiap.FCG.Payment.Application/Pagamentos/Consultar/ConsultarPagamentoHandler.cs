using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.Domain.Pagamentos;
using MediatR;

namespace Fiap.FCG.Payment.Application.Pagamentos.Consultar
{
    public class ConsultarPagamentoHandler : IRequestHandler<ConsultarPagamentoQuery, Result<PagamentoDetalheResponse>>
    {
        private readonly IPagamentoRepository _repository;

        public ConsultarPagamentoHandler(IPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagamentoDetalheResponse>> Handle(ConsultarPagamentoQuery request, CancellationToken cancellationToken)
        {
            var pagamento = await _repository.ObterPorIdAsync(request.PagamentoId);

            if (pagamento == null)
                return Result.Failure<PagamentoDetalheResponse>("Pagamento não encontrado.");

            var response = new PagamentoDetalheResponse
            {
                Id = pagamento.Id,
                UsuarioId = pagamento.UsuarioId,
                Valor = pagamento.ValorTotal,
                Status = pagamento.Status.ToString(),
                CriadoEm = pagamento.CriadoEm
            };

            return Result.Success(response);
        }
    }
}
