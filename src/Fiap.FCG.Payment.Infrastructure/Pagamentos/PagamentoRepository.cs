using Fiap.FCG.Payment.Domain.Pagamentos;
using Fiap.FCG.Payment.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.Infrastructure.Pagamentos
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PaymentDbContext _context;

        public PagamentoRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<Pagamento> ObterPorIdAsync(int id)
        {
            return await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
