namespace Fiap.App.Aluno.Application.Model
{
    public class AlunoTurmaDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}