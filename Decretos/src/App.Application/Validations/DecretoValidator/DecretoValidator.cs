using App.Application.Interfaces.Repository;
using FluentValidation;

namespace App.Application.Validations;

public class DecretoValidator: AbstractValidator<CriarDecretoDto>
{        
    private readonly IDecretoRepository _repository;

    public DecretoValidator(IDecretoRepository repository)
    {
        _repository = repository;
        
        RuleFor(d => d.Solicitante)
            .NotEmpty()
            .WithMessage("Solicitante é obrigatório.")
            .MinimumLength(5)
            .WithMessage("Solicitante deve ter no mínimo 5 caracteres.");

        RuleFor(d => d.DataParaUso)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Data para uso não pode ser anterior a hoje.");

        RuleFor(d => d.Secretaria)
            .IsInEnum()
            .WithMessage("Secretaria inválida.");

        RuleFor(d => d.Justificativa)
            .NotEmpty()
            .WithMessage("Justificativa é obrigatória.");
    }

}