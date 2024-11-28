using Fiap.App.Aluno.Domain.Entidades;

namespace Fiap.App.Aluno.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task AddTurmaAsync(Domain.Entidades.Turma turma);
        Task<IEnumerable<Domain.Entidades.Turma>> GetAllTurmasAsync();
        Task<Domain.Entidades.Turma> GetTurmaByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entidades.Turma>> GetByNomeAsync(string nome);
        Task UpdateAsync(Domain.Entidades.Turma turma);
        Task DeactivateAsync(Guid id);
    }
}