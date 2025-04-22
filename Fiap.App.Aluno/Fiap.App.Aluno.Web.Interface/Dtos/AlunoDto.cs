using System.ComponentModel.DataAnnotations;

namespace Fiap.App.Aluno.Web.Interface.Dtos
{
    public class AlunoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres.")]
        public string Nome { get; set; } = null!;
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(45, ErrorMessage = "O usuário deve ter no máximo 45 caracteres.")]
        public string Usuario { get; set; } = null!;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(60, ErrorMessage = "A senha deve ter no máximo 60 caracteres.")]
        public string Senha { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}