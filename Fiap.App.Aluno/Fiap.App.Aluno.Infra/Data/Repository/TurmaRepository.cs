using Dapper;
using Fiap.App.Aluno.Domain.Interfaces;
using Fiap.App.Aluno.Infra.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Fiap.App.Aluno.Infra.Data.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ApplicationDbContext context;

        public TurmaRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            this.context = context;
        }
        public async Task AddTurmaAsync(Domain.Entidades.Turma turma)
        {
            await context.Turmas.AddAsync(turma);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entidades.Turma>> GetAllTurmasAsync()
        {
            var query = "SELECT * FROM Turmas";
            return await dbConnection.QueryAsync<Domain.Entidades.Turma>(query);
        }

        public async Task<Domain.Entidades.Turma> GetTurmaByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Turmas WHERE Id = @id";
            return await dbConnection.QueryFirstOrDefaultAsync<Domain.Entidades.Turma>(query, new { Id = id });
        }
    }
}