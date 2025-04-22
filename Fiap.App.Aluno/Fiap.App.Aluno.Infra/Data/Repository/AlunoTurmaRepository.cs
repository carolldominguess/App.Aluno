using Dapper;
using Fiap.App.Aluno.Domain.Entidades;
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
            var query = @"
                    INSERT INTO aluno_turma (Id, AlunoId, TurmaId, Ativo, DataCriacao)
                    VALUES (@Id, @AlunoId, @TurmaId, @Ativo, @DataCriacao)";

            await dbConnection.ExecuteAsync(query, new
            {
                alunoTurma.Id,
                alunoTurma.AlunoId,
                alunoTurma.TurmaId,
                alunoTurma.Ativo,
                alunoTurma.DataCriacao
            });
        }

        public async Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAllAlunosTurmasAsync()
        {
            var query = "SELECT * FROM aluno_turma";
            return await dbConnection.QueryAsync<Domain.Entidades.AlunoTurma>(query);
        }

        public async Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAlunoTurmasByAlunoIdAsync(Guid alunoId)
        {
            var query = "SELECT * FROM aluno_turma WHERE AlunoId = @alunoId";
            return await dbConnection.QueryAsync<Domain.Entidades.AlunoTurma>(query, new { AlunoId = alunoId });
        }

        public async Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAlunoTurmasByTurmaIdAsync(Guid turmaId)
        {
            var query = "SELECT * FROM aluno_turma WHERE TurmaId = @turmaId";
            return await dbConnection.QueryAsync<Domain.Entidades.AlunoTurma>(query, new { TurmaId = turmaId });
        }

        public async Task<Domain.Entidades.AlunoTurma> GetAlunoTurmaByAlunoIdAndTurmaIdAsync(Guid alunoId, Guid turmaId)
        {
            var query = "SELECT * FROM aluno_turma WHERE AlunoId = @alunoId AND TurmaId = @turmaId";
            return await dbConnection.QueryFirstOrDefaultAsync<Domain.Entidades.AlunoTurma>(query, new { AlunoId = alunoId, TurmaId = turmaId });
        }

        public async Task DeactivateAsync(Guid alunoId, Guid turmaId)
        {
            var query = @"
                UPDATE aluno_turma
                SET Ativo = @Ativo
                WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";

            await dbConnection.ExecuteAsync(query, new
            {
                Ativo = false,
                AlunoId = alunoId,
                TurmaId = turmaId
            });
        }

        public async Task UpdateAlunoTurmaAsync(AlunoTurma alunoTurma)
        {
            var query = @"
                UPDATE aluno_turma
                SET AlunoId = @AlunoId, 
                    TurmaId = @TurmaId, 
                    DataModificacao = @DataModificacao,
                    Ativo = @Ativo
                WHERE AlunoId = @OriginalAlunoId AND TurmaId = @OriginalTurmaId";

            await dbConnection.ExecuteAsync(query, new
            {
                alunoTurma.AlunoId,
                alunoTurma.TurmaId,
                alunoTurma.DataModificacao,
                alunoTurma.Ativo,
                OriginalAlunoId = alunoTurma.Id,
                OriginalTurmaId = alunoTurma.TurmaId
            });
        }
    }
}