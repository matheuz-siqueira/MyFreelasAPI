using FluentValidation;
using myfreelas.Dtos.User;

namespace myfreelas.Validators;

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
