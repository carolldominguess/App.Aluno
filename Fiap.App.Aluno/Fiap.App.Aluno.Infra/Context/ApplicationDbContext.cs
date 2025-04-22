using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Fiap.App.Aluno.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Domain.Entidades.Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunoTurma> AlunosTurmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TurmaMap());
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new AlunoTurmaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}