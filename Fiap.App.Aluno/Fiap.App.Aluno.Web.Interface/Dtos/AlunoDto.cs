namespace Fiap.App.Aluno.Web.Interface.Dtos
{
    public class AlunoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}