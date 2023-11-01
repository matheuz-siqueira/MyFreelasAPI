using FluentValidation;
using MyFreelas.Application.Dtos.User;

namespace MyFreelas.Application.Validators;

public class UpdatePasswordValidator : AbstractValidator<RequestUpdatePasswordJson>
{
    public UpdatePasswordValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty()
                .WithMessage("Senha atual não pode ser vazia"); 

        RuleFor(r => r.NewPassword)
            .NotEmpty()
                .WithMessage("Nova senha não pode ser vazia")
            .MinimumLength(6)
                .WithMessage("Nova senha precisa ter no mínimo 6 caracteres"); 
    }
}