namespace Fiap.App.Aluno.Domain.Entidades
{
    public class Turma : Entity
    {
        public Turma(Guid id, DateTime dataCriacao, DateTime? dataModificacao, Guid cursoId, string nome, int ano) : base(id, dataCriacao, dataModificacao)
        {
            CursoId = cursoId;
            Nome = nome;
            Ano = ano;
        }

        public Guid CursoId { get; private set; }
        public string Nome { get; private set; }
        public int Ano { get; private set; }
    }
}