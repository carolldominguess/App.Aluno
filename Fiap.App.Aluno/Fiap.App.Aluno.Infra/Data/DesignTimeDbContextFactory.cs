using Fiap.App.Aluno.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fiap.App.Aluno.Infra.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly string connectionString = "Server=(localDb)\\MSSQLLocalDB;Database=App.Aluno;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DesignTimeDbContextFactory()
        {

        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Configura o DbContextOptionsBuilder com a string de conexão
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}