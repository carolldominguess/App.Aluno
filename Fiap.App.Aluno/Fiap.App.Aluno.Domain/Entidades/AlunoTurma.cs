namespace Fiap.App.Aluno.Domain.Entidades
{
    public class AlunoTurma : Entity
    {
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
    }
}