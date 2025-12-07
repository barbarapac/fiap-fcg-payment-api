using Fiap.FCG.Payment.Domain.Pagamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.FCG.Payment.Infrastructure.Pagamentos
{
    public class TransacaoPagamentoConfiguration : IEntityTypeConfiguration<TransacaoPagamento>
    {
        public void Configure(EntityTypeBuilder<TransacaoPagamento> builder)
        {
            builder.ToTable("TransacaoPagamento");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.CodigoTransacao)
                .IsRequired();

            builder.Property(t => t.Provedor)
                .IsRequired();

            builder.Property(t => t.DataRegistro)
                .IsRequired();
        }
    }
}
