namespace Fiap.App.Aluno.Application.Utils
{
    public class ResultadoOperacao
    {
        public bool Sucesso { get; }
        public string Mensagem { get; }
        public IEnumerable<string> Erros { get; }

        public ResultadoOperacao(bool sucesso, string mensagem, IEnumerable<string> erros = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Erros = erros ?? new List<string>();
        }
    }
}