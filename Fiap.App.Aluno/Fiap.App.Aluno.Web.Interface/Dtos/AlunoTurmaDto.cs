using System.ComponentModel.DataAnnotations;

namespace Fiap.App.Aluno.Web.Interface.Dtos
{
    public class AlunoTurmaDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Aluno é obrigatório.")]
        public Guid? AlunoId { get; set; }
        [Required(ErrorMessage = "O campo Turma é obrigatório.")]
        public Guid? TurmaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}