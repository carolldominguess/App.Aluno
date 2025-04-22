using System.ComponentModel.DataAnnotations;

namespace Fiap.App.Aluno.Web.Interface.Dtos
{
    public class TurmaDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 45 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Ano é obrigatório.")]
        public int? Ano { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}