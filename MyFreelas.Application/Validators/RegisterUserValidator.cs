using FluentValidation;
using MyFreelas.Application.Dtos.User;

namespace MyFreelas.Application.Validators;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
                .WithMessage("Nome não pode ser vazio")
            .MinimumLength(3)
                .WithMessage("Nome deve ter no mínimo 3 caracteres");

        RuleFor(u => u.LastName)
            .NotEmpty()
                .WithMessage("Sobrenome não pode ser vazio")
            .MinimumLength(3)
                .WithMessage("Sobrenome precisa ter no mínimo 3 caracteres"); 

        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email não pode ser vazio")
            .EmailAddress()
                .WithMessage("O endereço de email precisa ser válido");
        
        RuleFor(u => u.Password)
            .NotEmpty()
                .WithMessage("Senha não pode ser vazia") 
            .MinimumLength(6)
                .WithMessage("Senha deve possuir no mínimo 6 caracteres");   
    }
}
