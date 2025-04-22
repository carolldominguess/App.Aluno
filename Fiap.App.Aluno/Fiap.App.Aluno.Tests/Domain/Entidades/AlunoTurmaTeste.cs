using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Domain.Entidades.Validations;
using FluentAssertions;

namespace Fiap.App.Aluno.Tests.Domain.Entidades
{
    public class AlunoTurmaTeste
    {
        private readonly AlunoTurmaValidator _validator;

        public AlunoTurmaTeste()
        {
            _validator = new AlunoTurmaValidator();
        }

        [Fact]
        public void AlunoTurma_DeveSerInstanciadaCorretamente()
        {
            var alunoId = Guid.NewGuid();
            var turmaId = Guid.NewGuid();

            var alunoTurma = new AlunoTurma
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            };

            alunoTurma.AlunoId.Should().Be(alunoId);
            alunoTurma.TurmaId.Should().Be(turmaId);
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoAlunoIdForVazio()
        {
            var alunoTurma = new AlunoTurma
            {
                AlunoId = Guid.Empty, // AlunoId vazio
                TurmaId = Guid.NewGuid()
            };

            var resultado = _validator.Validate(alunoTurma);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.PropertyName == nameof(AlunoTurma.AlunoId));
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoTurmaIdForVazio()
        {
            var alunoTurma = new AlunoTurma
            {
                AlunoId = Guid.NewGuid(),
                TurmaId = Guid.Empty
            };

            var resultado = _validator.Validate(alunoTurma);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.PropertyName == nameof(AlunoTurma.TurmaId));
        }

        [Fact]
        public void Validator_DeveValidarCorretamente_QuandoDadosForemValidos()
        {
            var alunoTurma = new AlunoTurma
            {
                AlunoId = Guid.NewGuid(),
                TurmaId = Guid.NewGuid()
            };

            var resultado = _validator.Validate(alunoTurma);

            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_DeveValidarRapidamente()
        {
            var alunoTurma = new AlunoTurma
            {
                AlunoId = Guid.NewGuid(),
                TurmaId = Guid.NewGuid()
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var resultado = _validator.Validate(alunoTurma);
            stopwatch.Stop();

            resultado.IsValid.Should().BeTrue();
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(10, "a validação deve ser rápida.");
        }
    }
}