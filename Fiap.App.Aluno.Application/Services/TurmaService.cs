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

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<ResultadoOperacao> AddTurmaAsync(TurmaDto turmaDto)
        {
            // Verifica se já existe uma turma com o mesmo nome
            var turmaExistente = await _turmaRepository.GetByNomeAsync(turmaDto.Nome);
            if (turmaExistente != null && turmaExistente.Any())
            {
                return new ResultadoOperacao(false, "Já existe uma turma com este nome.");
            }

            // Cria a turma e salva
            var turma = new Turma
            {
                Id = Guid.NewGuid(),
                Nome = turmaDto.Nome,
                Ano = turmaDto.Ano
            };

            // Valida os dados da turma
            var validationResult = new TurmaValidator().Validate(turma);

            if (!validationResult.IsValid)
            {
                return new ResultadoOperacao(false, "Turma invalida: " + string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            await _turmaRepository.AddTurmaAsync(turma);
            return new ResultadoOperacao(true, "Turma adicionada com sucesso.");
        }

        public async Task<IEnumerable<TurmaDto>> GetAllTurmasAsync()
        {
            var turmas = await _turmaRepository.GetAllTurmasAsync();
            return turmas.Select(t => new TurmaDto { Nome = t.Nome, Ano = t.Ano });
        }

        public async Task<TurmaDto> GetTurmaByIdAsync(Guid id)
        {
            var turma = await _turmaRepository.GetTurmaByIdAsync(id);
            if (turma == null) return null;

            return new TurmaDto { Nome = turma.Nome, Ano = turma.Ano };
        }

        //public async Task<ResultadoOperacao> UpdateTurmaAsync(Guid id, TurmaDto turmaDto)
        //{
        //    // Valida os dados da turma
        //    var validationResult = await _validator.ValidateAsync(turmaDto);
        //    if (!validationResult.IsValid)
        //    {
        //        return new ResultadoOperacao(false, string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        //    }

        //    var turmaExistente = await _turmaRepository.GetTurmaByIdAsync(id);
        //    if (turmaExistente == null)
        //    {
        //        return new ResultadoOperacao(false, "Turma não encontrada.");
        //    }

        //    // Verifica se o nome já está em uso por outra turma
        //    var turmaComMesmoNome = await _turmaRepository.GetByNomeAsync(turmaDto.Nome);
        //    if (turmaComMesmoNome != null && turmaComMesmoNome.Id != id)
        //    {
        //        return new ResultadoOperacao(false, "Já existe uma turma com este nome.");
        //    }

        //    // Atualiza os dados da turma
        //    turmaExistente.Nome = turmaDto.Nome;
        //    turmaExistente.Ano = turmaDto.Ano;

        //    await _turmaRepository.UpdateAsync(turmaExistente);
        //    return new ResultadoOperacao(true, "Turma atualizada com sucesso.");
        //}

        //public async Task<ResultadoOperacao> DeleteTurmaAsync(Guid id)
        //{
        //    var turma = await _turmaRepository.GetTurmaByIdAsync(id);
        //    if (turma == null)
        //    {
        //        return new ResultadoOperacao(false, "Turma não encontrada.");
        //    }

        //    await _turmaRepository.DeleteAsync(turma);
        //    return new ResultadoOperacao(true, "Turma removida com sucesso.");
        //}
    }
}