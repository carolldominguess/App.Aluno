using Dapper;
using Fiap.App.Aluno.Domain.Interfaces;
using Fiap.App.Aluno.Infra.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Fiap.App.Aluno.Infra.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ApplicationDbContext context;

        public AlunoRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            this.context = context;
        }

        public async Task AddAlunoAsync(Domain.Entidades.Aluno aluno)
        {
            var query = @"
                INSERT INTO Alunos (Id, Nome, Usuario, Senha, Ativo)
                VALUES (@Id, @Nome, @Usuario, @Senha, @Ativo)";

            await dbConnection.ExecuteAsync(query, new
            {
                aluno.Id,
                aluno.Nome,
                aluno.Usuario,
                aluno.Senha,
                aluno.Ativo
            });
        }

        public async Task<IEnumerable<Domain.Entidades.Aluno>> GetAllAlunosAsync()
        {
            var query = "SELECT * FROM Alunos";
            return await dbConnection.QueryAsync<Domain.Entidades.Aluno>(query);
        }

        public async Task<Domain.Entidades.Aluno> GetAlunoByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Alunos WHERE Id = @id";
            return await dbConnection.QueryFirstOrDefaultAsync<Domain.Entidades.Aluno>(query, new { Id = id });
        }

        public async Task UpdateAlunoAsync(Domain.Entidades.Aluno aluno)
        {
            var query = @"
                UPDATE Alunos
                SET Nome = @Nome,
                    Usuario = @Usuario,
                    Senha = @Senha
                WHERE Id = @Id";

            await dbConnection.ExecuteAsync(query, new
            {
                aluno.Nome,
                aluno.Usuario,
                aluno.Senha,
                aluno.Id
            });
        }

        public async Task DeactivateAlunoAsync(Guid id)
        {
            var query = @"
                UPDATE Alunos
                SET Ativo = @Ativo
                WHERE Id = @Id";

            await dbConnection.ExecuteAsync(query, new
            {
                Ativo = false,
                Id = id
            });
        }
    }
}