using Fiap.App.Aluno.Application.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Fiap.App.Aluno.Application.Utils
{
    public class SenhaHasher : ISenhaHasher
    {
        public string CriarHash(string senha)
        {
            // Gera um salt aleatório para adicionar ao hash
            byte[] salt = new byte[16];
            new Random().NextBytes(salt);

            // Realiza o hash da senha usando PBKDF2
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Retorna o hash junto com o salt, assim é possível validá-lo posteriormente
            return $"{Convert.ToBase64String(salt)}:{hash}";
        }

        public bool VerificarSenha(string senha, string hashArmazenado)
        {
            var partes = hashArmazenado.Split(':');
            byte[] salt = Convert.FromBase64String(partes[0]);
            string hashEsperado = partes[1];

            // Realiza o cálculo do hash da senha informada
            string hashCalculado = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashEsperado == hashCalculado;
        }
    }
}