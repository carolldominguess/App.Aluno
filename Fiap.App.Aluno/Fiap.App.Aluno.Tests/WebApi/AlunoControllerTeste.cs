using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.App.Aluno.Tests.WebApi.Controllers
{
    public class AlunoControllerTeste
    {
        private readonly Mock<IAlunoService> _alunoServiceMock;
        private readonly AlunoController _controller;

        public AlunoControllerTeste()
        {
            _alunoServiceMock = new Mock<IAlunoService>();
            _controller = new AlunoController(_alunoServiceMock.Object);
        }

        [Fact]
        public async Task AddAluno_DeveRetornarOk_QuandoSucesso()
        {
            var alunoComTurmaDto = new AlunoComTurmaDto
            {
                Aluno = new AlunoDto { Nome = "João", Usuario = "joaosilva", Senha = "SenhaForte123" },
                AlunoTurma = new AlunoTurmaDto { TurmaId = Guid.NewGuid() }
            };

            _alunoServiceMock.Setup(s => s.AddAlunoAsync(alunoComTurmaDto.Aluno, alunoComTurmaDto.AlunoTurma))
                             .ReturnsAsync(new ResultadoOperacao(true, "Aluno adicionado com sucesso."));

            var result = await _controller.AddAluno(alunoComTurmaDto);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Aluno adicionado com sucesso.");
        }

        [Fact]
        public async Task AddAluno_DeveRetornarBadRequest_QuandoFalha()
        {
            var alunoComTurmaDto = new AlunoComTurmaDto
            {
                Aluno = new AlunoDto { Nome = "João", Usuario = "joaosilva", Senha = "SenhaFraca" },
                AlunoTurma = new AlunoTurmaDto { TurmaId = Guid.NewGuid() }
            };

            _alunoServiceMock.Setup(s => s.AddAlunoAsync(alunoComTurmaDto.Aluno, alunoComTurmaDto.AlunoTurma))
                             .ReturnsAsync(new ResultadoOperacao(false, "Erro ao adicionar aluno."));

            var result = await _controller.AddAluno(alunoComTurmaDto);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Erro ao adicionar aluno.");
        }

        [Fact]
        public async Task UpdateAluno_DeveRetornarOk_QuandoSucesso()
        {
            var alunoDto = new AlunoDto { Nome = "João Atualizado", Usuario = "joaoupdate" };
            var id = Guid.NewGuid();

            _alunoServiceMock.Setup(s => s.UpdateAlunoAsync(id, alunoDto))
                             .ReturnsAsync(new ResultadoOperacao(true, "Aluno atualizado com sucesso."));

            var result = await _controller.UpdateAluno(id, alunoDto);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Aluno atualizado com sucesso.");
        }

        [Fact]
        public async Task UpdateAluno_DeveRetornarBadRequest_QuandoFalha()
        {
            var alunoDto = new AlunoDto { Nome = "João Atualizado", Usuario = "joaoupdate" };
            var id = Guid.NewGuid();

            _alunoServiceMock.Setup(s => s.UpdateAlunoAsync(id, alunoDto))
                             .ReturnsAsync(new ResultadoOperacao(false, "Erro ao atualizar aluno."));

            var result = await _controller.UpdateAluno(id, alunoDto);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Erro ao atualizar aluno.");
        }

        [Fact]
        public async Task GetAllAlunos_DeveRetornarOk_QuandoExistiremAlunos()
        {
            var alunos = new List<AlunoDto>
            {
                new AlunoDto { Nome = "João", Usuario = "joaosilva" },
                new AlunoDto { Nome = "Maria", Usuario = "mariasouza" }
            };

            _alunoServiceMock.Setup(s => s.GetAllAlunosAsync()).ReturnsAsync(alunos);

            var result = await _controller.GetAllAlunos();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(alunos);
        }

        [Fact]
        public async Task GetAllAlunos_DeveRetornarNotFound_QuandoNaoExistiremAlunos()
        {
            _alunoServiceMock.Setup(s => s.GetAllAlunosAsync())
                             .ThrowsAsync(new KeyNotFoundException("Nenhum aluno encontrado."));

            var result = await _controller.GetAllAlunos();

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("Nenhum aluno encontrado.");
        }

        [Fact]
        public async Task GetAlunoById_DeveRetornarOk_QuandoEncontrado()
        {
            var id = Guid.NewGuid();
            var aluno = new AlunoDto { Nome = "João", Usuario = "joaosilva" };

            _alunoServiceMock.Setup(s => s.GetAlunoByIdAsync(id)).ReturnsAsync(aluno);

            var result = await _controller.GetAlunoById(id);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(aluno);
        }

        [Fact]
        public async Task GetAlunoById_DeveRetornarNotFound_QuandoNaoEncontrado()
        {
            var id = Guid.NewGuid();

            _alunoServiceMock.Setup(s => s.GetAlunoByIdAsync(id))
                             .ThrowsAsync(new KeyNotFoundException("Aluno não encontrado."));

            var result = await _controller.GetAlunoById(id);

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("Aluno não encontrado.");
        }

        [Fact]
        public async Task DeactivateAluno_DeveRetornarOk_QuandoSucesso()
        {
            var id = Guid.NewGuid();

            _alunoServiceMock.Setup(s => s.DeactivateAlunoAsync(id))
                             .ReturnsAsync(new ResultadoOperacao(true, "Aluno desativado com sucesso."));

            var result = await _controller.DeactivateAluno(id);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Aluno desativado com sucesso.");
        }

        [Fact]
        public async Task DeactivateAluno_DeveRetornarBadRequest_QuandoFalha()
        {
            var id = Guid.NewGuid();

            _alunoServiceMock.Setup(s => s.DeactivateAlunoAsync(id))
                             .ReturnsAsync(new ResultadoOperacao(false, "Erro ao desativar aluno."));

            var result = await _controller.DeactivateAluno(id);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Erro ao desativar aluno.");
        }
    }
}