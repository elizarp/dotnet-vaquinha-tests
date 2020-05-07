using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Repository.Mapping
{
    public class DoacaoDetalheTransacaoMapping : IEntityTypeConfiguration<DoacaoDetalheTransacao>
    {
        public void Configure(EntityTypeBuilder<DoacaoDetalheTransacao> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Success)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(e => e.ErrorCode)
                .IsRequired(false);

            builder.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(e => e.IsDuplicated)
                .IsRequired(false);

            builder.Property(e => e.InvoiceId)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(e => e.InvoiceUrl)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(e => e.Metodo)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(e => e.OrderId)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(e => e.ElapsedMillisecondsTransacao)
                .IsRequired(true);

            builder.HasOne(d => d.Doacao)
                .WithOne(d => d.DetalheTransacao)
                .HasForeignKey<Doacao>(d => d.DetalheTransacaoId);

            builder.ToTable("DoacaoDetalheTransacao");
        }
    }
}