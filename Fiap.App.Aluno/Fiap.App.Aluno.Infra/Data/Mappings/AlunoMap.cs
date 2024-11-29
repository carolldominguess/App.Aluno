using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.App.Aluno.Infra.Data.Mappings
{
    public class AlunoMap : IEntityTypeConfiguration<Domain.Entidades.Aluno>
    {
        public void Configure(EntityTypeBuilder<Domain.Entidades.Aluno> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Usuario)
                .HasColumnName("usuario")
                .IsRequired()
                .HasColumnType("varchar(45)");

            builder.Property(p => p.Senha)
                .HasColumnName("senha")
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(t => t.Ativo)
                .HasColumnType("bit")
                .IsRequired();

            builder.ToTable("alunos");
        }
    }
}