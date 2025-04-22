using Fiap.App.Aluno.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.App.Aluno.Infra.Data.Mappings
{
    public class AlunoTurmaMap : IEntityTypeConfiguration<Domain.Entidades.AlunoTurma>
    {
        public void Configure(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.HasKey(at => at.Id);

            builder
                .HasOne<Domain.Entidades.Aluno>()
                .WithMany()
                .HasForeignKey(at => at.AlunoId);

            builder
                .HasOne<Turma>()
                .WithMany()
                .HasForeignKey(at => at.TurmaId);

            builder.Property(t => t.Ativo)
                .HasColumnType("bit")
                .IsRequired();

            builder.ToTable("aluno_turma");
        }
    }
}