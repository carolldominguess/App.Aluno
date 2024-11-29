using AutoMapper;
using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;
using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Domain.Entidades.Validations;
using Fiap.App.Aluno.Domain.Interfaces;

namespace Fiap.App.Aluno.Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMapper _mapper;

        public TurmaService(ITurmaRepository turmaRepository, IMapper mapper)
        {
            _turmaRepository = turmaRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> AddTurmaAsync(TurmaDto turmaDto)
        {
            try
            {
                var turma = _mapper.Map<Turma>(turmaDto);

                var turmaExistente = await _turmaRepository.GetByNomeAsync(turma.Nome);
                if (turmaExistente != null && turmaExistente.Any())
                {
                    return new ResultadoOperacao(false, "Já existe uma turma com este nome.");
                }

                var validationResult = new TurmaValidator().Validate(turma);

                if (!validationResult.IsValid)
                {
                    return new ResultadoOperacao(false, "Turma invalida: " + string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                turma.DataCriacao = DateTime.Now;
                turma.Ativo = true;

                await _turmaRepository.AddTurmaAsync(turma);
                return new ResultadoOperacao(true, "Turma adicionada com sucesso.");
            }
            catch (Exception ex)
            {
                return new ResultadoOperacao(false, "Erro: " + string.Join("; ", ex.InnerException?.Message, ex.StackTrace));
            }
            
        }

        public async Task<IEnumerable<TurmaDto>> GetAllTurmasAsync()
        {
            var turmas = await _turmaRepository.GetAllTurmasAsync();
            return _mapper.Map<IEnumerable<TurmaDto>>(turmas);
        }

        public async Task<TurmaDto> GetTurmaByIdAsync(Guid id)
        {
            var turma = await _turmaRepository.GetTurmaByIdAsync(id);
            if (turma == null) return null;

            return _mapper.Map<TurmaDto>(turma);
        }

        public async Task<ResultadoOperacao> UpdateTurmaAsync(Guid id, TurmaDto turmaDto)
        {
            var turmaExistente = await _turmaRepository.GetTurmaByIdAsync(id);
            if (turmaExistente == null)
            {
                return new ResultadoOperacao(false, "Turma não encontrada.");
            }

            var turmaComMesmoNome = await _turmaRepository.GetByNomeAsync(turmaDto.Nome);
            if (turmaComMesmoNome != null && turmaComMesmoNome.Select(t => t.Id).Contains(id))
            {
                return new ResultadoOperacao(false, "Já existe uma turma com este nome.");
            }

            turmaExistente.Nome = turmaDto.Nome;
            turmaExistente.Ano = turmaDto.Ano;
            turmaExistente.DataModificacao = DateTime.Now;
            turmaExistente.Ativo = true;

            var validationResult = new TurmaValidator().Validate(turmaExistente);

            if (!validationResult.IsValid)
            {
                return new ResultadoOperacao(false, "Turma invalida: " + string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            await _turmaRepository.UpdateAsync(turmaExistente);
            return new ResultadoOperacao(true, "Turma atualizada com sucesso.");
        }

        public async Task<ResultadoOperacao> DeactivateTurmaAsync(Guid id)
        {
            var turma = await _turmaRepository.GetTurmaByIdAsync(id);
            if (turma == null)
            {
                return new ResultadoOperacao(false, "Turma não encontrada.");
            }

            turma.DataModificacao = DateTime.Now;

            await _turmaRepository.DeactivateAsync(id);
            return new ResultadoOperacao(true, "Turma desativada com sucesso.");
        }
    }
}