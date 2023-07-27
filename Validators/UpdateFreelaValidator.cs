using FluentValidation;
using myfreelas.Dtos.Freela;

namespace myfreelas.Validators;

public class UpdateFreelaValidator : AbstractValidator<RequestUpdateFreelaJson>
{
    public UpdateFreelaValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
                .WithMessage("Nome não pode ser vazio")
            .MinimumLength(3)
                .WithMessage("Nome do projeto precisa ter no mínimo 3 caracteres");

        RuleFor(f => f.Contract.Price)
            .NotEmpty().WithMessage("Valor do projeto não pode ser nulo")
            .GreaterThan(0).WithMessage("O valor do projeto deve ser maior que zero");

        RuleFor(f => f.Contract.StartDate)
            .NotEmpty().WithMessage("Data de início do projeto não pode ser nula");

        RuleFor(f => f.Contract.FinishDate)
            .NotEmpty().WithMessage("Data de conclusão do projeto não pode ser nula");

        RuleFor(f => f.Contract.FinishDate).GreaterThan(f => f.Contract.StartDate)
            .WithMessage("Data de conclusão precisa ser maior que data de início");

        RuleFor(f => f.Contract.PaymentInstallment)
            .InclusiveBetween(1, 12).WithMessage("Parcelas dever ser de no mínimo 1x e no máximo 12x");
    }
}
