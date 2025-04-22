using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ISenhaHasher _senhaHasher;
        private readonly ISenhaValidator _senhaValidator;

        public AlunoService(IAlunoRepository alunoRepository, IMapper mapper, ISenhaHasher senhaHasher, ISenhaValidator senhaValidator)
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
            _senhaHasher = senhaHasher;
            _senhaValidator = senhaValidator;
        }

        public async Task<ResultadoOperacao> AddAlunoAsync(AlunoDto alunoDto)
        {
            try
            {
                if (!_senhaValidator.ValidarSenha(new string(alunoDto.Senha)))
                {
                    return new ResultadoOperacao(false, "A senha não atende aos requisitos de segurança.");
                }

                var hashSenha = _senhaHasher.CriarHash(alunoDto.Senha);

                var aluno = _mapper.Map<Domain.Entidades.Aluno>(alunoDto);

                var validate = new AlunoValidator().Validate(aluno);

                if (!validate.IsValid)
                {
                    return new ResultadoOperacao(false, "Aluno invalido: " + string.Join("; ", validate.Errors.Select(e => e.ErrorMessage)));
                }

                aluno.Id = Guid.NewGuid();
                aluno.DataCriacao = DateTime.Now;
                aluno.Ativo = true;
                aluno.Senha = hashSenha;

                await _alunoRepository.AddAlunoAsync(aluno);

                return new ResultadoOperacao(true, "Aluno adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return new ResultadoOperacao(false, "Ocorreu uma exceção não tratada");
            }            
        }

        public async Task<ResultadoOperacao> UpdateAlunoAsync(Guid id, AlunoDto alunoDto)
        {
            try
            {
                var alunoExistente = await _alunoRepository.GetAlunoByIdAsync(id);
                if (alunoExistente == null)
                {
                    return new ResultadoOperacao(false, "Aluno não encontrado.");
                }

                if (alunoDto.Senha != null && !_senhaValidator.ValidarSenha(new string(alunoDto.Senha)))
                {
                    return new ResultadoOperacao(false, "A senha não atende aos requisitos de segurança.");
                }

                var alunoAtualizado = _mapper.Map<Domain.Entidades.Aluno>(alunoDto);
                alunoAtualizado.Id = id;

                if (alunoDto.Senha != null)
                {
                    alunoAtualizado.Senha = _senhaHasher.CriarHash(new string(alunoDto.Senha));
                }
                else
                {
                    alunoAtualizado.Senha = alunoExistente.Senha;
                }

                var validate = new AlunoValidator().Validate(alunoAtualizado);
                if (!validate.IsValid)
                {
                    return new ResultadoOperacao(false, "Aluno inválido: " + string.Join("; ", validate.Errors.Select(e => e.ErrorMessage)));
                }

                alunoAtualizado.DataModificacao = DateTime.Now;
                alunoAtualizado.Ativo = true;

                await _alunoRepository.UpdateAlunoAsync(alunoAtualizado);
                return new ResultadoOperacao(true, "Aluno atualizado com sucesso.");
            }
            catch (Exception)
            {
                return new ResultadoOperacao(false, "Ocorreu uma exceção não tratada");
            }            
        }

        public async Task<AlunoDto> GetAlunoByIdAsync(Guid id)
        {
            var aluno = await _alunoRepository.GetAlunoByIdAsync(id);
            if (aluno == null)
            {
                throw new KeyNotFoundException("Aluno não encontrado.");
            }

            return _mapper.Map<AlunoDto>(aluno);
        }

        public async Task<IEnumerable<AlunoDto>> GetAllAlunosAsync()
        {
            var alunos = await _alunoRepository.GetAllAlunosAsync();
            if (alunos == null || !alunos.Any())
            {
                throw new KeyNotFoundException("Nenhum aluno encontrado.");
            }

            return _mapper.Map<IEnumerable<AlunoDto>>(alunos);
        }

        public async Task<ResultadoOperacao> DeactivateAlunoAsync(Guid id)
        {
            var alunoExistente = await _alunoRepository.GetAlunoByIdAsync(id);
            if (alunoExistente == null)
            {
                return new ResultadoOperacao(false, "Aluno não encontrado.");
            }

            await _alunoRepository.DeactivateAlunoAsync(id);

            return new ResultadoOperacao(true, "Aluno desativado com sucesso.");
        }
    }
}