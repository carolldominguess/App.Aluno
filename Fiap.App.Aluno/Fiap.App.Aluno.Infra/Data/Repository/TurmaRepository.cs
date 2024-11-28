using Dapper;
using Fiap.App.Aluno.Domain.Entidades;
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
            var query = @"
                INSERT INTO Turmas (Id, Nome, Ano, Ativo)
                VALUES (@Id, @Nome, @Ano, @Ativo)";

            await dbConnection.ExecuteAsync(query, new
            {
                turma.Id,
                turma.Nome,
                turma.Ano,
                turma.Ativo
            });
        }

        public async Task DeactivateAsync(Guid id)
        {
            var query = @"
                    UPDATE turmas
                    SET Ativo = @Ativo
                    WHERE Id = @Id";

            await dbConnection.ExecuteAsync(query, new
            {
                Ativo = false,
                Id = id
            });
        }

        public async Task<IEnumerable<Domain.Entidades.Turma>> GetAllTurmasAsync()
        {
            var query = "SELECT * FROM turmas";
            return await dbConnection.QueryAsync<Domain.Entidades.Turma>(query);
        }

        public async Task<IEnumerable<Domain.Entidades.Turma>> GetByNomeAsync(string nome)
        {
            var query = "SELECT nome FROM turmas WHERE nome = @nome";
            var resultado = await dbConnection.QueryAsync<Domain.Entidades.Turma>(query, new { Nome = nome });
            return resultado ?? Enumerable.Empty<Domain.Entidades.Turma>();
        }

        public async Task<Domain.Entidades.Turma> GetTurmaByIdAsync(Guid id)
        {
            var query = "SELECT * FROM turmas WHERE Id = @id";
            return await dbConnection.QueryFirstOrDefaultAsync<Domain.Entidades.Turma>(query, new { Id = id });
        }

        public async Task UpdateAsync(Turma turma)
        {
            var query = @"
                UPDATE turmas
                SET nome = @Nome,
                    ano = @Ano
                WHERE Id = @Id";

            await dbConnection.ExecuteAsync(query, new
            {
                turma.Nome,
                turma.Ano,
                turma.Id
            });
        }
    }
}