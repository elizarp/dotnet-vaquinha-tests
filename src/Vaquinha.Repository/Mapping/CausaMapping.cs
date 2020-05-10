using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Repository.Mapping
{
    public class CausaMapping : IEntityTypeConfiguration<Causa>
    {
        public void Configure(EntityTypeBuilder<Causa> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Nome)
                 .IsRequired()
                 .HasMaxLength(150);

            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(2);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Causa");
        }
    }
}