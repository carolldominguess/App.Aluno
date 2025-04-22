using Fiap.App.Aluno.Domain.Entidades.Validations;
using FluentAssertions;

namespace Fiap.App.Aluno.Tests.Domain.Entidades
{
    public class AlunoTeste
    {
        private readonly AlunoValidator _validator;

        public AlunoTeste()
        {
            _validator = new AlunoValidator();
        }
        [Fact]
        public void Aluno_DeveSerInstanciadoCorretamente()
        {
            var nome = "João da Silva";
            var usuario = "joaosilva";
            var senha = "SenhaForte123";

            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = nome,
                Usuario = usuario,
                Senha = senha
            };

            aluno.Nome.Should().Be(nome);
            aluno.Usuario.Should().Be(usuario);
            aluno.Senha.Should().Be(senha);
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoNomeForVazio()
        {
            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = "",
                Usuario = "usuario",
                Senha = "senhaForte"
            };

            var resultado = _validator.Validate(aluno);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nome do aluno é obrigatório.");
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoNomeForMuitoCurto()
        {
            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = "Jo",
                Usuario = "usuario",
                Senha = "senhaForte"
            };

            var resultado = _validator.Validate(aluno);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nome deve ter entre 3 e 100 caracteres.");
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoUsuarioForVazio()
        {
            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = "João da Silva",
                Usuario = "",
                Senha = "senhaForte"
            };

            var resultado = _validator.Validate(aluno);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O Usuario é obrigatório.");
        }

        [Fact]
        public void Validator_DeveRetornarErro_QuandoUsuarioForMuitoLongo()
        {
            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = "João da Silva",
                Usuario = new string('a', 46),
                Senha = "senhaForte"
            };

            var resultado = _validator.Validate(aluno);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O Usuario deve até 45 caracteres.");
        }

        [Fact]
        public void Validator_DeveValidarCorretamente_QuandoDadosForemValidos()
        {
            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = "João da Silva",
                Usuario = "joaosilva",
                Senha = "senhaForte"
            };

            var resultado = _validator.Validate(aluno);

            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_DeveValidarRapidamente()
        {
            var aluno = new App.Aluno.Domain.Entidades.Aluno
            {
                Nome = "João da Silva",
                Usuario = "joaosilva",
                Senha = "senhaForte"
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var resultado = _validator.Validate(aluno);
            stopwatch.Stop();

            resultado.IsValid.Should().BeTrue();
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(10);
        }
    }
}