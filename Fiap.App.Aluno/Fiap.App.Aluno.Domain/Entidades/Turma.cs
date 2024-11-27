namespace Fiap.App.Aluno.Domain.Entidades
{
    public class Turma : Entity
    {
        public Guid CursoId { get; set; }
        public string Nome { get; set; } = null!;
        public int Ano { get; set; }
    }
}