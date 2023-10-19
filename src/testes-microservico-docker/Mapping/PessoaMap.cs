using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestesMicroservicoDocker.Entities;

namespace TestesMicroservicoDocker.Mapping;

public class PessoaMap : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> entity)
    {
        entity.HasKey(e => e.Idpessoa).HasName("pk_pessoa");

        entity.ToTable("pessoa");

        entity.HasIndex(e => e.Documento, "uk_pessoa").IsUnique();

        entity.Property(e => e.Idpessoa).HasColumnName("idpessoa");
        entity.Property(e => e.Datafundacao)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("datafundacao");
        entity.Property(e => e.Datanascimento)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("datanascimento");
        entity.Property(e => e.Documento)
            .HasMaxLength(20)
            .HasColumnName("documento");
        entity.Property(e => e.Idpessoatipo).HasColumnName("idpessoatipo");
        entity.Property(e => e.Nomecompleto)
            .HasMaxLength(100)
            .HasColumnName("nomecompleto");
        entity.Property(e => e.Nomeresumido)
            .HasMaxLength(100)
            .HasColumnName("nomeresumido");

        entity.HasOne(d => d.IdpessoatipoNavigation).WithMany(p => p.Pessoas)
            .HasForeignKey(d => d.Idpessoatipo)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_pessoa_pessoatipo");
    }
}