using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.Domain.Pagamentos;
using MediatR;

namespace Fiap.FCG.Payment.Application.Pagamentos.Criar
{
    public class CriarPagamentoHandler : IRequestHandler<CriarPagamentoCommand, Result<int>>
    {
        private readonly IPagamentoRepository _repository;

        public CriarPagamentoHandler(IPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CriarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var pagamentoResult = Pagamento.Criar(
                request.CompraId,
                request.UsuarioId,
                request.ValorTotal,
                request.MetodoPagamento,
                request.BandeiraCartao
                );

            if (!pagamentoResult.Sucesso)
                return Result.Failure<int>(pagamentoResult.Erro);

            var pagamento = pagamentoResult.Valor;

            await _repository.AdicionarAsync(pagamento);

            return Result.Success(pagamento.Id);
        }
    }
}
