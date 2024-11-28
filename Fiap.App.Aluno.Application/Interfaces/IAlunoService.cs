using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;

namespace Fiap.App.Aluno.Application.Interfaces
{
    public interface IAlunoService
    {
        Task<ResultadoOperacao> AddAlunoAsync(AlunoDto alunoDto, AlunoTurmaDto alunoTurmaDto);
        Task<ResultadoOperacao> UpdateAlunoAsync(Guid id, AlunoDto alunoDto);
        Task<ResultadoOperacao> DeactivateAlunoAsync(Guid id);
        Task<Domain.Entidades.Aluno> GetAlunoByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entidades.Aluno>> GetAllAlunosAsync();
    }
}