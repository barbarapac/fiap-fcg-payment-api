using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Domain.Pagamentos
{
    public class Pagamento : Base
    {
        public int UsuarioId { get; private set; }
        public int JogoId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public EStatusPagamento Status { get; private set; }

        public List<TransacaoPagamento> Transacoes { get; private set; } = new();

        private Pagamento() { }

        public static Result<Pagamento> Criar(int usuarioId, int jogoId, decimal valor)
        {
            if (usuarioId <= 0)
                return Result.Failure<Pagamento>("Usuário inválido.");
            if (jogoId <= 0)
                return Result.Failure<Pagamento>("Jogo inválido.");
            if (valor <= 0)
                return Result.Failure<Pagamento>("Valor deve ser maior que zero.");

            var pagamento = new Pagamento
            {
                UsuarioId = usuarioId,
                JogoId = jogoId,
                Valor = valor,
                CriadoEm = DateTime.UtcNow,
                Status = EStatusPagamento.Pendente
            };

            return Result.Success(pagamento);
        }

        public Result<bool> Aprovar()
        {
            if (Status != EStatusPagamento.Pendente)
                return Result.Failure<bool>("Pagamento não está pendente.");

            Status = EStatusPagamento.Aprovado;
            return Result.Success(true);
        }

        public Result<bool> Recusar()
        {
            if (Status != EStatusPagamento.Pendente)
                return Result.Failure<bool>("Pagamento não está pendente.");

            Status = EStatusPagamento.Recusado;
            return Result.Success(true);
        }

        public Result<TransacaoPagamento> RegistrarTransacao(string codigo, string provedor)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return Result.Failure<TransacaoPagamento>("Código inválido.");

            if (string.IsNullOrWhiteSpace(provedor))
                return Result.Failure<TransacaoPagamento>("Provedor inválido.");

            var transacao = new TransacaoPagamento(Id, codigo, provedor);
            Transacoes.Add(transacao);

            return Result.Success(transacao);
        }
    }
}
