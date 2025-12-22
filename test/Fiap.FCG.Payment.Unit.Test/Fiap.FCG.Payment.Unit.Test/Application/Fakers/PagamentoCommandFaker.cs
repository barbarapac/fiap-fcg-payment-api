using Bogus;
using Fiap.FCG.Payment.Application.Pagamentos.Criar;
using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Unit.Test.Application.Fakers
{
    public static class PagamentoCommandFaker
    {
        private static readonly Faker _faker = new();


        public static CriarPagamentoCommand Valido() => new()
        {
            CompraId = _faker.Random.Int(1, 1000),
            UsuarioId = _faker.Random.Int(1, 1000),
            ValorTotal = _faker.Random.Decimal(1, 5000),
            MetodoPagamento = EMetodoPagamento.Credito,
            BandeiraCartao = EBandeiraCartao.Visa
        };


        public static CriarPagamentoCommand Invalido() => new()
        {
            CompraId = 0,
            UsuarioId = -1,
            ValorTotal = 0,
            MetodoPagamento = 0,
            BandeiraCartao = null
        };
    }
}
