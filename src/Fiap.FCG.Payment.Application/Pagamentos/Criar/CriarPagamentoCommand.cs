using Fiap.FCG.Payment.Domain._Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Fiap.FCG.Payment.Application.Pagamentos.Criar
{
    public class CriarPagamentoCommand : IRequest<Result<int>>
    {
        [Required(ErrorMessage = "O ID da compra é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Compra inválida.")]
        public int CompraId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Usuário inválido.")]
        public int UsuarioId { get; set; }        

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public EMetodoPagamento MetodoPagamento { get; set; }
        
        public EBandeiraCartao? BandeiraCartao { get; set; }
    }
}
