using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;
using Fiap.App.Aluno.Domain.Entidades.Validations;
using Fiap.App.Aluno.Domain.Interfaces;

namespace Fiap.App.Aluno.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly SenhaHasher _senhaHasher;
        private readonly SenhaValidator _senhaValidator;

        public AlunoService(IAlunoRepository alunoRepository, SenhaHasher senhaHasher, SenhaValidator senhaValidator)
        {
            _alunoRepository = alunoRepository;
            _senhaHasher = senhaHasher;
            _senhaValidator = senhaValidator;
        }

        // ALUNO TEM QUE SER ADICIONADO COM A TURMA NO ALUNO TURMA
        public async Task<ResultadoOperacao> AddAlunoAsync(AlunoDto alunoDto)
        {
            // Valida se a senha é forte
            if (!_senhaValidator.ValidarSenha(new string(alunoDto.Senha)))
            {
                return new ResultadoOperacao(false, "A senha não atende aos requisitos de segurança.");
            }

            // Hash da senha
            var hashSenha = _senhaHasher.CriarHash(new string(alunoDto.Senha));            

            var aluno = new Domain.Entidades.Aluno
            {
                Nome = alunoDto.Nome,
                Senha = hashSenha,
                Ativo = true
            };

            var validate = new AlunoValidator().Validate(aluno);

            if (!validate.IsValid)
            {
                return new ResultadoOperacao(false, "Aluno invalido: " + string.Join("; ", validate.Errors.Select(e => e.ErrorMessage)));
            }

            await _alunoRepository.AddAlunoAsync(aluno);
            return new ResultadoOperacao(true, "Aluno adicionado com sucesso");
        }
    }
}