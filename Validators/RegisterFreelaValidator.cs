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
        
        RuleFor(f => f.Value)
            .NotEmpty()
                .WithMessage("Valor do projeto não pode ser nulo")
            .GreaterThan(0)
                .WithMessage("O valor do projeto precisa ser maior que zero"); 
        
        RuleFor(f => f.StartDate).NotEmpty()
            .WithMessage("Data de início do projeto não pode ser nula");

        RuleFor(f => f.FinishDate).NotEmpty()
            .WithMessage("Data de conclusão do projeto não pode ser nula"); 

        RuleFor(f => f.FinishDate).GreaterThan(f => f.StartDate)
            .WithMessage("Data de conclusão precisa ser maior que data de início"); 
    }
}
