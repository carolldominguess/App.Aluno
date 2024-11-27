using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;

namespace Fiap.App.Aluno.Application.Interfaces
{
    public interface IAlunoService
    {
        Task<ResultadoOperacao> AddAlunoAsync(AlunoDto alunoDto);
    }
}