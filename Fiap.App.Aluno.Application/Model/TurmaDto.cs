namespace Fiap.App.Aluno.Application.Model
{
    public class TurmaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public int Ano { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}