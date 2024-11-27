namespace Fiap.App.Aluno.Domain.Interfaces
{
    public interface IAlunoTurmaRepository
    {
        Task AddAlunoTurmaAsync(Domain.Entidades.AlunoTurma alunoTurma);
        Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAllAlunosTurmasAsync();
        Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAlunoTurmasByAlunoIdAsync(Guid alunoId);
        Task<IEnumerable<Domain.Entidades.AlunoTurma>> GetAlunoTurmasByTurmaIdAsync(Guid turmaId);
        Task<Domain.Entidades.AlunoTurma> GetAlunoTurmaByAlunoIdAndTurmaIdAsync(Guid alunoId, Guid turmaId);
    }
}