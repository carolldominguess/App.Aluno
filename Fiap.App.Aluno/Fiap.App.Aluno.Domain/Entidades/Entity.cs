namespace Fiap.App.Aluno.Domain.Entidades
{
    public abstract class Entity
    {
        protected Entity(Guid id, DateTime dataCriacao, DateTime? dataModificacao)
        {
            Id = id;
            DataCriacao = dataCriacao;
            DataModificacao = dataModificacao;
        }

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
    }
}