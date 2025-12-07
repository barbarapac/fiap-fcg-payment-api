using System.ComponentModel.DataAnnotations;

namespace Fiap.FCG.Payment.Application.Pagamentos.Criar
{
    public class CriarPagamentoDto
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Usuário inválido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O meio de pagamento é obrigatório.")]
        public string MeioPagamento { get; set; } = "cartao_credito";

        [Required(ErrorMessage = "Lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "A compra deve conter ao menos 1 item.")]
        public List<ItemPagamentoDto> Itens { get; set; } = new();
    }
}
