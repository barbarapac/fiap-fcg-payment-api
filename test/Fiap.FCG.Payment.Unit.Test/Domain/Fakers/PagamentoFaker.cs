using AutoBogus;
using Bogus;
using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Unit.Test.Domain.Fakers
{
    public static class PagamentoFaker
    {
        private static readonly Faker _faker = new();

        public static int CompraIdValido => _faker.Random.Int(1, 1000);
        public static int UsuarioIdValido => _faker.Random.Int(1, 1000);
        public static decimal ValorValido => _faker.Random.Decimal(1, 1000);
        public static EBandeiraCartao BandeiraValida => _faker.PickRandom<EBandeiraCartao>();
    }
}
