using Fiap.App.Aluno.Application.Notificacoes;

namespace Fiap.App.Aluno.Application.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}