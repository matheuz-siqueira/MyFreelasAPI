using FluentValidation;
using HashidsNet;
using myfreelas.Dtos.Freela;

namespace myfreelas.Validators;

public class RegisterFreelaValidator : AbstractValidator<RequestRegisterFreelaJson>
{

    public RegisterFreelaValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
                .WithMessage("Nome não pode ser vazio")
            .MinimumLength(3)
                .WithMessage("Nome do projeto precisa ter no mínimo 3 caracteres");

        RuleFor(f => f.Contract.Price).NotEmpty().WithMessage("Preço não pode ser vazio")
        .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

        RuleFor(f => f.Contract.StartDate).NotEmpty().WithMessage("Data de início não pode ser vazio");

        RuleFor(f => f.Contract.FinishDate).NotEmpty().WithMessage("Data de conclusão não pode ser vazio");

        RuleFor(f => f.Contract.FinishDate).GreaterThan(f => f.Contract.StartDate)
            .WithMessage("Data de conclusão deve ser maior que data de início");

        RuleFor(f => f.Contract.PaymentInstallment)
            .NotEmpty().WithMessage("Parcela(s) não pode(m) ser nula(s)")
            .InclusiveBetween(1, 12).WithMessage("Parcelas devem estar entre 1x e 12x");

        RuleFor(f => f.CustomerId).NotEmpty().WithMessage("Informe o id do cliente");
    }
}
