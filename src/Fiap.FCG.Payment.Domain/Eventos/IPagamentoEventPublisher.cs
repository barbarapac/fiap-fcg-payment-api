using Fiap.FCG.Payment.Domain.Pagamentos;

namespace Fiap.FCG.Payment.Domain.Eventos
{
    public interface IPagamentoEventPublisher
    {
        Task PublicarPagamentoCriadoAsync(Pagamento pagamento);
    }
}
