using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestesMicroservicoDocker.Entities;

namespace TestesMicroservicoDocker.Mapping;

public class PessoatipoMap : IEntityTypeConfiguration<Pessoatipo>
{
    public void Configure(EntityTypeBuilder<Pessoatipo> entity)
    {
        entity.HasKey(e => e.Idpessoatipo).HasName("pk_pessoatipo");

        entity.ToTable("pessoatipo");

        entity.HasIndex(e => e.Descricao, "uk_pessoatipo").IsUnique();

        entity.Property(e => e.Idpessoatipo).HasColumnName("idpessoatipo");
        entity.Property(e => e.Descricao)
            .HasMaxLength(100)
            .HasColumnName("descricao");
        entity.Property(e => e.Tag)
            .HasMaxLength(100)
            .HasColumnName("tag");
    }
}