using Fiap.App.Aluno.Application.Interfaces;
using System.Text.RegularExpressions;

namespace Fiap.App.Aluno.Application.Utils
{
    public class SenhaValidator : ISenhaValidator
    {
        public bool ValidarSenha(string senha)
        {
            if (senha.Length < 8 && senha.Length > 60)
                return false;

            var temMaiuscula = new Regex(@"[A-Z]+");
            var temMinuscula = new Regex(@"[a-z]+");
            var temNumero = new Regex(@"[0-9]+");
            var temEspecial = new Regex(@"[\W_]+");

            return temMaiuscula.IsMatch(senha) &&
                   temMinuscula.IsMatch(senha) &&
                   temNumero.IsMatch(senha) &&
                   temEspecial.IsMatch(senha);
        }
    }
}