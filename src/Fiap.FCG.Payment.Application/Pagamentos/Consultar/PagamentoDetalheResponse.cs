namespace Fiap.FCG.Payment.Application.Pagamentos.Consultar
{
    public class PagamentoDetalheResponse
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
