namespace Fiap.App.Aluno.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task AddTurmaAsync(Domain.Entidades.Turma turma);
        Task<IEnumerable<Domain.Entidades.Turma>> GetAllTurmasAsync();
        Task<Domain.Entidades.Turma> GetTurmaByIdAsync(Guid id);
        Task<IEnumerable<string>> GetByNomeAsync(string nome);
    }
}