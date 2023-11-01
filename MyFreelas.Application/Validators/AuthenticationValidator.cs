using FluentValidation;
using MyFreelas.Application.Dtos.User;

namespace MyFreelas.Application.Validators;

public class AuthenticationValidator : AbstractValidator<RequestAuthenticationJson>
{
    public AuthenticationValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email não pode ser vazio")
            .EmailAddress()
                .WithMessage("Email precisa ser válido");  

        RuleFor(u => u.Password)
            .NotEmpty()
                .WithMessage("Senha não pode ser vazio");
    }
}