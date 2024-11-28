using AutoMapper;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Services;
using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Fiap.App.Aluno.Tests.Application.Services
{
    public class TurmaServiceTeste
    {
        private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TurmaService _turmaService;

        public TurmaServiceTeste()
        {
            _turmaRepositoryMock = new Mock<ITurmaRepository>();
            _mapperMock = new Mock<IMapper>();

            _turmaService = new TurmaService(_turmaRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddTurmaAsync_DeveRetornarErro_SeNomeJaExistir()
        {
            var turmaDto = new TurmaDto { Nome = "Turma A", Ano = 2023 };
            var turma = new Turma { Nome = "Turma A", Ano = 2023 };

            _mapperMock.Setup(m => m.Map<Turma>(turmaDto)).Returns(turma);
            _turmaRepositoryMock.Setup(r => r.GetByNomeAsync(turma.Nome)).ReturnsAsync(new List<Turma> { turma });

            var resultado = await _turmaService.AddTurmaAsync(turmaDto);

            resultado.Sucesso.Should().BeFalse();
            resultado.Mensagem.Should().Be("Já existe uma turma com este nome.");
        }

        [Fact]
        public async Task AddTurmaAsync_DeveAdicionarTurma_SeDadosForemValidos()
        {
            var turmaDto = new TurmaDto { Nome = "Turma A", Ano = 2023 };
            var turma = new Turma { Nome = "Turma A", Ano = 2023 };

            _mapperMock.Setup(m => m.Map<Turma>(turmaDto)).Returns(turma);
            _turmaRepositoryMock.Setup(r => r.GetByNomeAsync(turma.Nome)).ReturnsAsync((IEnumerable<Turma>)null);
            _turmaRepositoryMock.Setup(r => r.AddTurmaAsync(turma)).Returns(Task.CompletedTask);

            var resultado = await _turmaService.AddTurmaAsync(turmaDto);

            resultado.Sucesso.Should().BeTrue();
            resultado.Mensagem.Should().Be("Turma adicionada com sucesso.");
        }

        [Fact]
        public async Task GetAllTurmasAsync_DeveRetornarListaDeTurmas()
        {
            var turmas = new List<Turma>
            {
                new Turma { Nome = "Turma A", Ano = 2023 },
                new Turma { Nome = "Turma B", Ano = 2024 }
            };

            _turmaRepositoryMock.Setup(r => r.GetAllTurmasAsync()).ReturnsAsync(turmas);
            _mapperMock.Setup(m => m.Map<IEnumerable<TurmaDto>>(turmas))
                       .Returns(turmas.Select(t => new TurmaDto { Nome = t.Nome, Ano = t.Ano }));

            var resultado = await _turmaService.GetAllTurmasAsync();

            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTurmaByIdAsync_DeveRetornarTurmaCorreta()
        {
            var turmaId = Guid.NewGuid();
            var turma = new Turma { Nome = "Turma A", Ano = 2023 };

            _turmaRepositoryMock.Setup(r => r.GetTurmaByIdAsync(turmaId)).ReturnsAsync(turma);
            _mapperMock.Setup(m => m.Map<TurmaDto>(turma))
                       .Returns(new TurmaDto { Nome = turma.Nome, Ano = turma.Ano });

            var resultado = await _turmaService.GetTurmaByIdAsync(turmaId);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Turma A");
            resultado.Ano.Should().Be(2023);
        }

        [Fact]
        public async Task UpdateTurmaAsync_DeveRetornarErro_SeTurmaNaoExistir()
        {
            var turmaId = Guid.NewGuid();
            var turmaDto = new TurmaDto { Nome = "Nova Turma", Ano = 2024 };

            _turmaRepositoryMock.Setup(r => r.GetTurmaByIdAsync(turmaId)).ReturnsAsync((Turma)null);

            var resultado = await _turmaService.UpdateTurmaAsync(turmaId, turmaDto);

            resultado.Sucesso.Should().BeFalse();
            resultado.Mensagem.Should().Be("Turma não encontrada.");
        }

        [Fact]
        public async Task DeactivateTurmaAsync_DeveRetornarErro_SeTurmaNaoExistir()
        {
            var turmaId = Guid.NewGuid();

            _turmaRepositoryMock.Setup(r => r.GetTurmaByIdAsync(turmaId)).ReturnsAsync((Turma)null);

            var resultado = await _turmaService.DeactivateTurmaAsync(turmaId);

            resultado.Sucesso.Should().BeFalse();
            resultado.Mensagem.Should().Be("Turma não encontrada.");
        }

        [Fact]
        public async Task DeactivateTurmaAsync_DeveDesativarTurma()
        {
            var turmaId = Guid.NewGuid();
            var turma = new Turma { Nome = "Turma A", Ano = 2023, Ativo = true };

            _turmaRepositoryMock.Setup(r => r.GetTurmaByIdAsync(turmaId)).ReturnsAsync(turma);
            _turmaRepositoryMock.Setup(r => r.DeactivateAsync(turmaId)).Returns(Task.CompletedTask);

            var resultado = await _turmaService.DeactivateTurmaAsync(turmaId);

            resultado.Sucesso.Should().BeTrue();
            resultado.Mensagem.Should().Be("Turma desativada com sucesso.");
        }
    }
}