using Bogus;
using Fiap.FCG.Payment.Domain._Shared;
using Fiap.FCG.Payment.Domain.Pagamentos;

namespace Fiap.FCG.Payment.Unit.Test.Infrastructure.Fakers
{
    public static class PagamentoFaker
    {
        private static readonly Faker _faker = new();


        public static Pagamento Valido()
        {
            return Pagamento.Criar(
            _faker.Random.Int(1, 100),
            _faker.Random.Int(1, 100),
            _faker.Random.Decimal(1, 1000),
            EMetodoPagamento.Credito,
            EBandeiraCartao.Visa).Valor;
        }
    }
}
