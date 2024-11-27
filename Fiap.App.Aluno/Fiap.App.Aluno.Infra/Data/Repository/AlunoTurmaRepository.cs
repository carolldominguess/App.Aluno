using Dapper;
using Fiap.App.Aluno.Domain.Interfaces;
using Fiap.App.Aluno.Infra.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Fiap.App.Aluno.Infra.Data.Repository
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ApplicationDbContext context;

        public AlunoTurmaRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            this.context = context;
        }

        public async Task AddAlunoTurmaAsync(Domain.Entidades.AlunoTurma alunoTurma)
        {
            await context.AlunosTurmas.AddAsync(alunoTurma);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAllAlunosTurmasAsync()
        {
            var query = "SELECT * FROM AlunosTurmas";
            return await dbConnection.QueryAsync<Domain.Entidades.AlunoTurma>(query);
        }

        public async Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAlunoTurmasByAlunoIdAsync(Guid alunoId)
        {
            var query = "SELECT * FROM AlunosTurmas WHERE AlunoId = @alunoId";
            return await dbConnection.QueryAsync<Domain.Entidades.AlunoTurma>(query, new { AlunoId = alunoId });
        }

        public async Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAlunoTurmasByTurmaIdAsync(Guid turmaId)
        {
            var query = "SELECT * FROM AlunosTurmas WHERE TurmaId = @turmaId";
            return await dbConnection.QueryAsync<Domain.Entidades.AlunoTurma>(query, new { TurmaId = turmaId });
        }

        public async Task<Domain.Entidades.AlunoTurma> GetAlunoTurmaByAlunoIdAndTurmaIdAsync(Guid alunoId, Guid turmaId)
        {
            var query = "SELECT * FROM AlunosTurmas WHERE AlunoId = @alunoId AND TurmaId = @turmaId";
            return await dbConnection.QueryFirstOrDefaultAsync<Domain.Entidades.AlunoTurma>(query, new { AlunoId = alunoId, TurmaId = turmaId });
        }
    }
}