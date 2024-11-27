namespace Fiap.App.Aluno.Domain.Entidades
{
    public class Aluno : Entity
    {
        public string Nome { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public char[] Senha { get; set; } = null!;
    }
}