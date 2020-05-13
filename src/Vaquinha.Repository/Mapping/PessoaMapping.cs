using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Repository.Mapping
{
    public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Anonima)
                .IsRequired();

            builder.Property(e => e.MensagemApoio)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(p => p.Doacoes)
                .WithOne(d => d.DadosPessoais)
                .HasForeignKey(d => d.DadosPessoaisId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Pessoa");
        }
    }
}