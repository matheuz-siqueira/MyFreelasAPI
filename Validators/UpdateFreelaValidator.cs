using FluentValidation;
using myfreelas.Dtos.Freela;

namespace myfreelas.Validators;

public class UpdateFreelaValidator : AbstractValidator<RequestUpdateFreelaJson>
{
    public UpdateFreelaValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio")
            .MinimumLength(3).WithMessage("Nome do projeto precisa ter no mínimo 3 caracteres");

        RuleFor(f => f.Price)
            .NotEmpty().WithMessage("Valor do projeto não pode ser nulo")
            .GreaterThan(0).WithMessage("O valor do projeto deve ser maior que zero");

        RuleFor(f => f.StartDate)
            .NotEmpty().WithMessage("Data de início do projeto não pode ser nula");

        RuleFor(f => f.FinishDate)
            .NotEmpty().WithMessage("Data de conclusão do projeto não pode ser nula");

        RuleFor(f => f.FinishDate).GreaterThan(f => f.StartDate)
            .WithMessage("Data de conclusão precisa ser maior que data de início");

        RuleFor(f => f.StartPayment).GreaterThanOrEqualTo(f => f.StartDate);

        RuleFor(f => f.PaymentInstallment)
            .InclusiveBetween(1, 12).WithMessage("Parcelas dever ser de no mínimo 1x e no máximo 12x");
    }
}
