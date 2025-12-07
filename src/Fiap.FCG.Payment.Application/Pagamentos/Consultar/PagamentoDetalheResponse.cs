namespace Fiap.FCG.Payment.Application.Pagamentos.Consultar
{
    public class PagamentoDetalheResponse
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int JogoId { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public DateTime CriadoEm { get; set; }

        public List<TransacaoDto> Transacoes { get; set; } = new();

        public class TransacaoDto
        {
            public string Codigo { get; set; }
            public string Provedor { get; set; }
            public DateTime Data { get; set; }
        }
    }
}
