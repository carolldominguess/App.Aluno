using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;

namespace Fiap.App.Aluno.Application.Interfaces
{
    public interface IAlunoTurmaService
    {
        Task<ResultadoOperacao> AddAlunoTurmaAsync(AlunoTurmaDto alunoTurmaDto);
        Task<IEnumerable<AlunoTurmaDto>> GetAllAlunosTurmasAsync();
        Task<IEnumerable<AlunoDto>> GetAlunosByTurmaIdAsync(Guid turmaId);
        Task<IEnumerable<TurmaDto>> GetTurmasByAlunoIdAsync(Guid alunoId);
        Task<ResultadoOperacao> DeactivateAlunoTurmaAsync(Guid alunoId, Guid turmaId);
    }
}