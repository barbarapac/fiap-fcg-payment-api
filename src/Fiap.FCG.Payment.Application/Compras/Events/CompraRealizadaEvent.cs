using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Application.Compras.Events
{
    public class CompraRealizadaEvent
    {
        public int CompraId { get; set; }
        public int UsuarioId { get; set; }
        public decimal ValorTotal { get; set; }
        public EMetodoPagamento MetodoPagamento { get; set; } = default!;
        public EBandeiraCartao? BandeiraCartao { get; set; }
        public DateTime CriadaEm { get; set; }
    }
}
