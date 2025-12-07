using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.Domain.Eventos;
using Fiap.FCG.Payment.Domain.Pagamentos;
using MediatR;

namespace Fiap.FCG.Payment.Application.Pagamentos.Criar
{
    public class CriarPagamentoHandler : IRequestHandler<CriarPagamentoCommand, Result<int>>
    {
        private readonly IPagamentoRepository _repository;
        private readonly IPagamentoEventPublisher _publisher;

        public CriarPagamentoHandler(
            IPagamentoRepository repository,
            IPagamentoEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Result<int>> Handle(CriarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var pagamentoResult = Pagamento.Criar(request.UsuarioId, request.JogoId, request.Valor);

            if (!pagamentoResult.Sucesso)
                return Result.Failure<int>(pagamentoResult.Erro);

            var pagamento = pagamentoResult.Valor;

            await _repository.AdicionarAsync(pagamento);

            await _publisher.PublicarPagamentoCriadoAsync(pagamento);

            return Result.Success(pagamento.Id);
        }
    }
}
