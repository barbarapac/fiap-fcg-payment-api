using Fiap.FCG.Payment.Infrastructure._Shared;
using Fiap.FCG.Payment.Infrastructure.Pagamentos;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.Infrastructure
{
    public class PagamentoRepositoryTest
    {
        private readonly PagamentoRepository _repository;
        private readonly PaymentDbContext _context;


        public PagamentoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PaymentDbContext>()
            .UseInMemoryDatabase("PagamentoDb")
            .Options;


            _context = new PaymentDbContext(options);
            _repository = new PagamentoRepository(_context);
        }


        [Fact]
        public async Task AdicionarAsync_QuandoPagamentoValido_DevePersistirNoBanco()
        {
            // Arrange
            var pagamento = Fakers.PagamentoFaker.Valido();


            // Act
            await _repository.AdicionarAsync(pagamento);


            // Assert
            var persisted = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == pagamento.Id);
            persisted.Should().NotBeNull();
            persisted.ValorTotal.Should().Be(pagamento.ValorTotal);
        }


        [Fact]
        public async Task ObterPorIdAsync_QuandoExiste_DeveRetornarPagamento()
        {
            // Arrange
            var pagamento = Fakers.PagamentoFaker.Valido();
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();


            // Act
            var result = await _repository.ObterPorIdAsync(pagamento.Id);


            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(pagamento.Id);
        }


        [Fact]
        public async Task ObterPorIdAsync_QuandoNaoExiste_DeveRetornarNull()
        {
            // Act
            var result = await _repository.ObterPorIdAsync(-999);


            // Assert
            result.Should().BeNull();
        }
    }
}
