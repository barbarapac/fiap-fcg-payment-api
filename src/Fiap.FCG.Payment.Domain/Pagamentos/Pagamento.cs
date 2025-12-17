using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Domain.Pagamentos
{
    public class Pagamento : Base
    {
        public int CompraId { get; private set; }
        public int UsuarioId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime CriadoEm { get; private set; }

        public EMetodoPagamento MetodoPagamento { get; private set; }
        public EBandeiraCartao? BandeiraCartao { get; private set; }

        public EStatusPagamento Status { get; private set; }

        private Pagamento() { }

        public static Result<Pagamento> Criar(
            int compraId,
            int usuarioId,
            decimal valor,
            EMetodoPagamento metodoPagamento,
            EBandeiraCartao? bandeiraCartao)
        {
            if (compraId <= 0)
                return Result.Failure<Pagamento>("Compra inválida.");

            if (usuarioId <= 0)
                return Result.Failure<Pagamento>("Usuário inválido.");

            if (valor <= 0)
                return Result.Failure<Pagamento>("Valor deve ser maior que zero.");

            if (metodoPagamento == EMetodoPagamento.Credito && bandeiraCartao is null)
                return Result.Failure<Pagamento>("Bandeira do cartão é obrigatória para crédito.");

            var pagamento = new Pagamento
            {
                CompraId = compraId,
                UsuarioId = usuarioId,
                ValorTotal = valor,
                MetodoPagamento = metodoPagamento,
                BandeiraCartao = bandeiraCartao,
                CriadoEm = DateTime.UtcNow,
                Status = EStatusPagamento.Pendente
            };

            pagamento.Processar();

            return Result.Success(pagamento);
        }
        /// <summary>
        /// Simula o processamento do pagamento com base nas regras definidas.
        /// </summary>
        private void Processar()
        {
            if (MetodoPagamento == EMetodoPagamento.Pix)
            {
                Status = EStatusPagamento.Aprovado;
                return;
            }

            if (MetodoPagamento == EMetodoPagamento.Credito)
            {                
                if (BandeiraCartao == EBandeiraCartao.Elo)
                {
                    Status = EStatusPagamento.Recusado;
                    return;
                }

                Status = EStatusPagamento.Aprovado;
            }
        }
    }
}
