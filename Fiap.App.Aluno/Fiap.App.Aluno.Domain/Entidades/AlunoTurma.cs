namespace Fiap.App.Aluno.Domain.Entidades
{
    public class AlunoTurma : Entity
    {
        public AlunoTurma(Guid id, DateTime dataCriacao, DateTime? dataModificacao, Guid alunoId, Guid turmaId) : base(id, dataCriacao, dataModificacao)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }

        public Guid AlunoId { get; private set; }
        public Guid TurmaId { get; private set; }
    }
}