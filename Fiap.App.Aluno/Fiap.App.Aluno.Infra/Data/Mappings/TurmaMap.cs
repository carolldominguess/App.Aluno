using Fiap.App.Aluno.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.App.Aluno.Infra.Data.Mappings
{
    public class TurmaMap : IEntityTypeConfiguration<Domain.Entidades.Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType("varchar(45)");

            builder.Property(p => p.Ano)
                .HasColumnName("ano")
                .IsRequired();

            builder.Property(t => t.Ativo)
                .HasColumnType("bit")
                .IsRequired();

            builder.ToTable("turmas");
        }
    }
}