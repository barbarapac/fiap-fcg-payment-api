using System.ComponentModel.DataAnnotations;

namespace Fiap.FCG.Payment.Application.Pagamentos.Criar
{
    public class ItemPagamentoDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int JogoId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PrecoPago { get; set; }
    }
}
