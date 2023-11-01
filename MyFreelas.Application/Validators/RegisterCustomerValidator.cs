using System.Text.RegularExpressions;
using FluentValidation;
using MyFreelas.Application.Dtos.Customer;

namespace MyFreelas.Application.Validators;

public class RegisterCustomerValidator : AbstractValidator<RequestCustomerJson>
{
    public RegisterCustomerValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Nome não pode ser vazio");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email não pode ser vazio");
        When(c => !string.IsNullOrWhiteSpace(c.Email), () => 
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email precisa ser válido");
        });
        RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Telefone não pode ser vazio");
        When(c => !string.IsNullOrWhiteSpace(c.PhoneNumber), () => 
        {
            RuleFor(c => c.PhoneNumber).Custom((phone, context) => 
            {
                string valid = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(phone, valid);
                if(!isMatch)
                {
                    context
                    .AddFailure(
                    new FluentValidation.Results.ValidationFailure(nameof(phone), "Telefone precisa ser válido"));
                }
            });
        });

        RuleFor(c => c.Type).IsInEnum(); 
    }
}
