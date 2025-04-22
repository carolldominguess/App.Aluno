using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Domain.Entidades.Validations;
using FluentAssertions;

namespace Fiap.App.Aluno.Tests.Domain.Entidades
{
    public class TurmaTeste
    {
        private readonly TurmaValidator _validator;

        public TurmaTeste()
        {
            _validator = new TurmaValidator();
        }

        [Fact]
        public void Turma_DeveSerInstanciadaCorretamente()
        {
            var nome = "Turma A";
            var ano = 2023;

            var turma = new Turma
            {
                Nome = nome,
                Ano = ano
            };

            turma.Nome.Should().Be(nome);
            turma.Ano.Should().Be(ano);
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoNomeForVazio()
        {
            var turma = new Turma
            {
                Nome = "",
                Ano = 2023
            };

            var resultado = _validator.Validate(turma);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nome da turma é obrigatório.");
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoNomeForMuitoLongo()
        {
            var turma = new Turma
            {
                Nome = new string('a', 46),
                Ano = 2023
            };

            var resultado = _validator.Validate(turma);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nome deve ter até 45 caracteres.");
        }

        [Fact]
        public void Validator_DeveValidarCorretamente_QuandoDadosForemValidos()
        {
            var turma = new Turma
            {
                Nome = "Turma A",
                Ano = 2023
            };

            var resultado = _validator.Validate(turma);

            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_DeveValidarRapidamente()
        {
            var turma = new Turma
            {
                Nome = "Turma A",
                Ano = 2023
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var resultado = _validator.Validate(turma);
            stopwatch.Stop();

            resultado.IsValid.Should().BeTrue();
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(10, "a validação deve ser rápida.");
        }
    }
}