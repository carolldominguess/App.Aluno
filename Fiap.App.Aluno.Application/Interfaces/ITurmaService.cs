using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;

namespace Fiap.App.Aluno.Application.Interfaces
{
    public interface ITurmaService
    {
        Task<ResultadoOperacao> AddTurmaAsync(TurmaDto turmaDto);
        Task<IEnumerable<TurmaDto>> GetAllTurmasAsync();
        Task<TurmaDto> GetTurmaByIdAsync(Guid id);
        Task<ResultadoOperacao> UpdateTurmaAsync(Guid id, TurmaDto turmaDto);
        Task<ResultadoOperacao> DeactivateTurmaAsync(Guid id);
    }
}