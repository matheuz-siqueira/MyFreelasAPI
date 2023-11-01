using FluentValidation;
using MyFreelas.Application.Dtos.Dashboard;

namespace MyFreelas.Application.Validators;

public class GetMonthlyBillingValidator : AbstractValidator<RequestGetMonthlyBillingJson>
{
    public GetMonthlyBillingValidator()
    {
        RuleFor(r => r.DateYear)
            .NotEmpty().WithMessage("Campo ano não pode ser vazio")
            .InclusiveBetween(1900, 2999).WithMessage("Campo ano deve estar entre 1900 e 2999");

        RuleFor(r => r.DateMonth)
            .NotEmpty().WithMessage("Campo mês não pode ser vazio")
            .InclusiveBetween(1, 12).WithMessage("Campo mês deve estar entre 1 e 12");
    }
}

