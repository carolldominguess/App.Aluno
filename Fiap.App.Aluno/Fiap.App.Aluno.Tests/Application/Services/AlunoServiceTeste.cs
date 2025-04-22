using AutoMapper;
using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Services;
using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Fiap.App.Aluno.Tests.Application.Services
{
    public class AlunoServiceTeste
    {
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ISenhaHasher> _senhaHasherMock;
        private readonly Mock<ISenhaValidator> _senhaValidatorMock;
        private readonly AlunoService _alunoService;

        public AlunoServiceTeste()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _mapperMock = new Mock<IMapper>();
            _senhaHasherMock = new Mock<ISenhaHasher>();
            _senhaValidatorMock = new Mock<ISenhaValidator>();

            _alunoService = new AlunoService(
                _alunoRepositoryMock.Object,
                _mapperMock.Object,
                _senhaHasherMock.Object,
                _senhaValidatorMock.Object
            );
        }

        [Fact]
        public async Task AddAlunoAsync_DeveRetornarErro_SeSenhaInvalida()
        {
            var alunoDto = new AlunoDto { Nome = "João", Usuario = "joaosilva", Senha = "123" };
            var alunoTurmaDto = new AlunoTurmaDto { TurmaId = Guid.NewGuid() };

            _senhaValidatorMock.Setup(s => s.ValidarSenha(It.IsAny<string>())).Returns(false);

            var resultado = await _alunoService.AddAlunoAsync(alunoDto);

            resultado.Sucesso.Should().BeFalse();
            resultado.Mensagem.Should().Be("A senha não atende aos requisitos de segurança.");
        }

        [Fact]
        public async Task AddAlunoAsync_DeveRetornarErro_SeDadosInvalidos()
        {
            var alunoDto = new AlunoDto { Nome = "", Usuario = "joaosilva", Senha = "SenhaForte123" };
            var aluno = new App.Aluno.Domain.Entidades.Aluno { Nome = "", Usuario = "joaosilva", Senha = "SenhaForte123" };
            var alunoTurmaDto = new AlunoTurmaDto { TurmaId = Guid.NewGuid() };

            _senhaValidatorMock.Setup(s => s.ValidarSenha(It.IsAny<string>())).Returns(true);
            _mapperMock.Setup(m => m.Map<App.Aluno.Domain.Entidades.Aluno>(alunoDto)).Returns(aluno);

            var resultado = await _alunoService.AddAlunoAsync(alunoDto);

            resultado.Sucesso.Should().BeFalse();
            resultado.Mensagem.Should().StartWith("Aluno invalido:");
        }

        [Fact]
        public async Task AddAlunoAsync_DeveAdicionarAluno_ComDadosValidos()
        {
            var alunoDto = new AlunoDto { Nome = "João", Usuario = "joaosilva", Senha = "SenhaForte123" };
            var aluno = new App.Aluno.Domain.Entidades.Aluno { Nome = "João", Usuario = "joaosilva", Senha = "SenhaForte123" };
            var alunoTurmaDto = new AlunoTurmaDto { TurmaId = Guid.NewGuid() };
            var alunoTurma = new AlunoTurma { AlunoId = Guid.NewGuid(), TurmaId = alunoTurmaDto.TurmaId };

            _senhaValidatorMock.Setup(s => s.ValidarSenha(It.IsAny<string>())).Returns(true);
            _senhaHasherMock.Setup(s => s.CriarHash(It.IsAny<string>())).Returns("SenhaHash");
            _mapperMock.Setup(m => m.Map<App.Aluno.Domain.Entidades.Aluno>(alunoDto)).Returns(aluno);
            _mapperMock.Setup(m => m.Map<AlunoTurma>(alunoTurmaDto)).Returns(alunoTurma);
            _alunoRepositoryMock.Setup(a => a.AddAlunoAsync(aluno)).Returns(Task.CompletedTask);

            var resultado = await _alunoService.AddAlunoAsync(alunoDto);

            resultado.Sucesso.Should().BeTrue();
            resultado.Mensagem.Should().Be("Aluno adicionado com sucesso");
        }

        [Fact]
        public async Task GetAllAlunosAsync_DeveRetornarErro_SeNaoExistiremAlunos()
        {
            _alunoRepositoryMock.Setup(a => a.GetAllAlunosAsync()).ReturnsAsync(Enumerable.Empty<App.Aluno.Domain.Entidades.Aluno>());

            Func<Task> act = async () => await _alunoService.GetAllAlunosAsync();

            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Nenhum aluno encontrado.");
        }

        [Fact]
        public async Task DeactivateAlunoAsync_DeveDesativarAluno()
        {
            var alunoId = Guid.NewGuid();
            var aluno = new App.Aluno.Domain.Entidades.Aluno { Id = alunoId, Nome = "João", Usuario = "joaosilva", Ativo = true };
            var turmasRelacionadas = new List<AlunoTurma> { new AlunoTurma { AlunoId = alunoId, TurmaId = Guid.NewGuid() } };

            _alunoRepositoryMock.Setup(a => a.GetAlunoByIdAsync(alunoId)).ReturnsAsync(aluno);
            _alunoRepositoryMock.Setup(a => a.DeactivateAlunoAsync(alunoId)).Returns(Task.CompletedTask);

            var resultado = await _alunoService.DeactivateAlunoAsync(alunoId);

            resultado.Sucesso.Should().BeTrue();
            resultado.Mensagem.Should().Be("Aluno desativado com sucesso.");
        }
    }
}