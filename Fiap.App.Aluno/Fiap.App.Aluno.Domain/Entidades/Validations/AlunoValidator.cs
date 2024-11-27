using FluentValidation;

namespace Fiap.App.Aluno.Domain.Entidades.Validations
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do aluno é obrigatório.")
            .Length(3, 255).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Usuario)
            .NotEmpty().WithMessage("O Usuario é obrigatório.")
            .MaximumLength(45).WithMessage("O Usuario deve até 45 caracteres.");
        }
    }
}