using FluentValidation;

namespace Fiap.App.Aluno.Domain.Entidades.Validations
{
    public class TurmaValidator : AbstractValidator<Turma>
    {
        public TurmaValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da turma é obrigatório.")
            .MaximumLength(45).WithMessage("O nome deve ter até 45 caracteres.");
        }
    }
}