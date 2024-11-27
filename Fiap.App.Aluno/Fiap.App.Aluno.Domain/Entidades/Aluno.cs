namespace Fiap.App.Aluno.Domain.Entidades
{
    public class Aluno : Entity
    {
        public Aluno(Guid id, DateTime dataCriacao, DateTime? dataModificacao, string nome, string usuario, char[] senha) : base(id, dataCriacao, dataModificacao)
        {
            Nome = nome;
            Usuario = usuario;
            Senha = senha;
        }

        public string Nome { get; private set; }
        public string Usuario { get; private set; }
        public char[] Senha { get; private set; }
    }
}