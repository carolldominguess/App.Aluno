using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.App.Aluno.Tests.WebApi.Controllers
{
    public class TurmaControllerTeste
    {
        private readonly Mock<ITurmaService> _turmaServiceMock;
        private readonly TurmaController _controller;

        public TurmaControllerTeste()
        {
            _turmaServiceMock = new Mock<ITurmaService>();
            _controller = new TurmaController(_turmaServiceMock.Object);
        }

        [Fact]
        public async Task AddTurma_DeveRetornarOk_QuandoSucesso()
        {
            var turmaDto = new TurmaDto { Nome = "Turma A", Ano = 2023 };
            _turmaServiceMock.Setup(s => s.AddTurmaAsync(turmaDto))
                             .ReturnsAsync(new ResultadoOperacao(true, "Turma adicionada com sucesso."));

            var result = await _controller.AddTurma(turmaDto);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Turma adicionada com sucesso.");
        }

        [Fact]
        public async Task AddTurma_DeveRetornarBadRequest_QuandoFalha()
        {
            var turmaDto = new TurmaDto { Nome = "Turma A", Ano = 2023 };
            _turmaServiceMock.Setup(s => s.AddTurmaAsync(turmaDto))
                             .ReturnsAsync(new ResultadoOperacao(false, "Erro ao adicionar turma."));

            var result = await _controller.AddTurma(turmaDto);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Erro ao adicionar turma.");
        }

        [Fact]
        public async Task GetAllTurmas_DeveRetornarOk_ComListaDeTurmas()
        {
            var turmas = new List<TurmaDto>
            {
                new TurmaDto { Nome = "Turma A", Ano = 2023 },
                new TurmaDto { Nome = "Turma B", Ano = 2024 }
            };
            _turmaServiceMock.Setup(s => s.GetAllTurmasAsync()).ReturnsAsync(turmas);

            var result = await _controller.GetAllTurmas();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(turmas);
        }

        [Fact]
        public async Task GetTurmaById_DeveRetornarOk_QuandoEncontrada()
        {
            var id = Guid.NewGuid();
            var turma = new TurmaDto { Nome = "Turma A", Ano = 2023 };
            _turmaServiceMock.Setup(s => s.GetTurmaByIdAsync(id)).ReturnsAsync(turma);

            var result = await _controller.GetTurmaById(id);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(turma);
        }

        [Fact]
        public async Task GetTurmaById_DeveRetornarNotFound_QuandoNaoEncontrada()
        {
            var id = Guid.NewGuid();
            _turmaServiceMock.Setup(s => s.GetTurmaByIdAsync(id)).ReturnsAsync((TurmaDto)null);

            var result = await _controller.GetTurmaById(id);

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("Turma não encontrada.");
        }

        [Fact]
        public async Task UpdateTurma_DeveRetornarOk_QuandoSucesso()
        {
            var id = Guid.NewGuid();
            var turmaDto = new TurmaDto { Nome = "Nova Turma", Ano = 2024 };
            _turmaServiceMock.Setup(s => s.UpdateTurmaAsync(id, turmaDto))
                             .ReturnsAsync(new ResultadoOperacao(true, "Turma atualizada com sucesso."));

            var result = await _controller.UpdateTurma(id, turmaDto);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Turma atualizada com sucesso.");
        }

        [Fact]
        public async Task UpdateTurma_DeveRetornarBadRequest_QuandoFalha()
        {
            var id = Guid.NewGuid();
            var turmaDto = new TurmaDto { Nome = "Nova Turma", Ano = 2024 };
            _turmaServiceMock.Setup(s => s.UpdateTurmaAsync(id, turmaDto))
                             .ReturnsAsync(new ResultadoOperacao(false, "Erro ao atualizar turma."));

            var result = await _controller.UpdateTurma(id, turmaDto);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Erro ao atualizar turma.");
        }

        [Fact]
        public async Task DeactivateTurma_DeveRetornarOk_QuandoSucesso()
        {
            var id = Guid.NewGuid();
            _turmaServiceMock.Setup(s => s.DeactivateTurmaAsync(id))
                             .ReturnsAsync(new ResultadoOperacao(true, "Turma desativada com sucesso."));

            var result = await _controller.DeactivateTurma(id);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Turma desativada com sucesso.");
        }

        [Fact]
        public async Task DeactivateTurma_DeveRetornarBadRequest_QuandoFalha()
        {
            var id = Guid.NewGuid();
            _turmaServiceMock.Setup(s => s.DeactivateTurmaAsync(id))
                             .ReturnsAsync(new ResultadoOperacao(false, "Erro ao desativar turma."));

            var result = await _controller.DeactivateTurma(id);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Erro ao desativar turma.");
        }
    }
}