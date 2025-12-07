namespace Fiap.FCG.Payment.Domain.Pagamentos
{
    public interface IPagamentoRepository
    {
        Task AdicionarAsync(Pagamento pagamento);
        Task<Pagamento> ObterPorIdAsync(int id);
    }
}
