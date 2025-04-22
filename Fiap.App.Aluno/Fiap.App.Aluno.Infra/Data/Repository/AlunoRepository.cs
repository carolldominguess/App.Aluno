using Dapper;
using Fiap.App.Aluno.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fiap.App.Aluno.Infra.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnection _dbConnection;

        public AlunoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAlunoAsync(Domain.Entidades.Aluno aluno)
        {
            var query = @"
                INSERT INTO Alunos (Id, Nome, Usuario, Senha, Ativo, DataCriacao)
                VALUES (@Id, @Nome, @Usuario, @Senha, @Ativo, @DataCriacao)";

            await _dbConnection.ExecuteAsync(query, new
            {
                aluno.Id,
                aluno.Nome,
                aluno.Usuario,
                aluno.Senha,
                aluno.Ativo,
                aluno.DataCriacao
            });
        }

        public async Task<IEnumerable<Domain.Entidades.Aluno>> GetAllAlunosAsync()
        {
            var query = "SELECT * FROM Alunos";
            return await _dbConnection.QueryAsync<Domain.Entidades.Aluno>(query);
        }

        public async Task<Domain.Entidades.Aluno> GetAlunoByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Alunos WHERE Id = @id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Domain.Entidades.Aluno>(query, new { Id = id });
        }

        public async Task UpdateAlunoAsync(Domain.Entidades.Aluno aluno)
        {
            var query = @"
                UPDATE Alunos
                SET Nome = @Nome,
                    Usuario = @Usuario,
                    Senha = @Senha,
                    DataModificacao = @DataModificacao
                WHERE Id = @Id";

            await _dbConnection.ExecuteAsync(query, new
            {
                aluno.Nome,
                aluno.Usuario,
                aluno.Senha,
                aluno.DataModificacao,
                aluno.Id
            });
        }

        public async Task DeactivateAlunoAsync(Guid id)
        {
            var query = @"
                UPDATE Alunos
                SET Ativo = @Ativo
                WHERE Id = @Id";

            await _dbConnection.ExecuteAsync(query, new
            {
                Ativo = false,
                Id = id
            });
        }
    }
}