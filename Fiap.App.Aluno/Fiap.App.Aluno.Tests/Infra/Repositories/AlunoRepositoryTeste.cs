using Dapper;
using Fiap.App.Aluno.Infra.Data.Repository;
using FluentAssertions;
using Moq;
using Moq.Dapper;
using System.Data;

namespace Fiap.App.Aluno.Tests.Infra.Repositories
{
    public class AlunoRepositoryTeste
    {
        private readonly Mock<IDbConnection> _dbConnectionMock;
        private readonly AlunoRepository _repository;

        public AlunoRepositoryTeste()
        {
            _dbConnectionMock = new Mock<IDbConnection>();

            _repository = new AlunoRepository(_dbConnectionMock.Object);
        }

        [Fact]
        public async Task GetAllAlunosAsync_DeveRetornarListaDeAlunos()
        {
            var alunos = new List<App.Aluno.Domain.Entidades.Aluno>
            {
                new App.Aluno.Domain.Entidades.Aluno { Id = Guid.NewGuid(), Nome = "João", Usuario = "joaosilva", Senha = "SenhaForte123", Ativo = true },
                new App.Aluno.Domain.Entidades.Aluno { Id = Guid.NewGuid(), Nome = "Maria", Usuario = "mariasilva", Senha = "SenhaForte456", Ativo = true }
            };

            _dbConnectionMock.SetupDapperAsync(c => c.QueryAsync<App.Aluno.Domain.Entidades.Aluno>(It.IsAny<string>(), null, null, null, null))
                             .ReturnsAsync(alunos);

            var resultado = await _repository.GetAllAlunosAsync();

            resultado.Should().NotBeNullOrEmpty();
            resultado.Should().HaveCount(alunos.Count);
        }

        [Fact]
        public async Task GetAlunoByIdAsync_DeveRetornarAlunoCorreto()
        {
            var alunoId = Guid.NewGuid();
            var aluno = new App.Aluno.Domain.Entidades.Aluno { Id = alunoId, Nome = "João", Usuario = "joaosilva", Senha = "SenhaForte123", Ativo = true };

            _dbConnectionMock.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<App.Aluno.Domain.Entidades.Aluno>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                             .ReturnsAsync(aluno);

            var resultado = await _repository.GetAlunoByIdAsync(alunoId);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(aluno.Nome);
            resultado.Usuario.Should().Be(aluno.Usuario);
        }
    }
}