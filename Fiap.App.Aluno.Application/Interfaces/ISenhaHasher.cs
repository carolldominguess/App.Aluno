namespace Fiap.App.Aluno.Application.Interfaces
{
    public interface ISenhaHasher
    {
        string CriarHash(string senha);
        bool VerificarSenha(string senha, string hashArmazenado);
    }
}