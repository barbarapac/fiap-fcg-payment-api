using Fiap.FCG.Payment.Domain._Shared;

namespace Fiap.FCG.Payment.Unit.Test.Domain.Fixtures
{
    public class PagamentoFixture
    {
        protected int CompraId => Fakers.PagamentoFaker.CompraIdValido;
        protected int UsuarioId => Fakers.PagamentoFaker.UsuarioIdValido;
        protected decimal Valor => Fakers.PagamentoFaker.ValorValido;
        protected EBandeiraCartao BandeiraValida => Fakers.PagamentoFaker.BandeiraValida;
    }
}
