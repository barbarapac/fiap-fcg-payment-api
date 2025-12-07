using Fiap.FCG.Payment.Domain._Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Fiap.FCG.Payment.Application.Pagamentos.Criar
{
    public class CriarPagamentoCommand : IRequest<Result<int>>
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Usuário inválido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O jogo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Jogo inválido.")]
        public int JogoId { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }
    }
}
