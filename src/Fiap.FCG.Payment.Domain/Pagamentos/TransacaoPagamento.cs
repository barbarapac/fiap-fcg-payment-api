using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Domain.Pagamentos
{
    public class TransacaoPagamento : Base
    {
        public int PagamentoId { get; private set; }
        public string CodigoTransacao { get; private set; }
        public string Provedor { get; private set; }
        public DateTime DataRegistro { get; private set; }

        private TransacaoPagamento() { }

        internal TransacaoPagamento(int pagamentoId, string codigoTransacao, string provedor)
        {
            PagamentoId = pagamentoId;
            CodigoTransacao = codigoTransacao;
            Provedor = provedor;
            DataRegistro = DateTime.UtcNow;
        }
    }
}
