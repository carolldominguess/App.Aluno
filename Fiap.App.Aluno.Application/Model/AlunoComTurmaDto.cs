namespace Fiap.App.Aluno.Application.Model
{
    public class AlunoComTurmaDto
    {
        public AlunoDto Aluno { get; set; } = null!;
        public AlunoTurmaDto AlunoTurma { get; set; } = null!;
    }
}