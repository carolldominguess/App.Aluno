using FluentValidation;

namespace Fiap.App.Aluno.Domain.Entidades.Validations
{
    public class AlunoTurmaValidator : AbstractValidator<AlunoTurma>
    {
        public AlunoTurmaValidator()
        {
            RuleFor(x => x.AlunoId).NotEmpty();
            RuleFor(x => x.TurmaId).NotEmpty();
        }
    }
}