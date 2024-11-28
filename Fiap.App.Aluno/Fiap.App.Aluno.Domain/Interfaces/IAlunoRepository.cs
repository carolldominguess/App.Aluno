namespace Fiap.App.Aluno.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task AddAlunoAsync(Domain.Entidades.Aluno aluno);
        Task<Domain.Entidades.Aluno> GetAlunoByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entidades.Aluno>> GetAllAlunosAsync();
        Task UpdateAlunoAsync(Domain.Entidades.Aluno aluno);
        Task DeactivateAlunoAsync(Guid id);
    }
}