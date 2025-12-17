using Fiap.FCG.Payment.Domain.Pagamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.FCG.Payment.Infrastructure.Pagamentos
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamento");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UsuarioId)
                .IsRequired();

            builder.Property(p => p.ValorTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.CriadoEm)
                .IsRequired();

            builder.Property(p => p.MetodoPagamento)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.BandeiraCartao)
                .HasConversion<string>()
                .IsRequired(false);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .IsRequired();

        }
    }
}
