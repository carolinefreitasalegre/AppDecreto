using App.Application.Interfaces.Repository;
using FluentValidation;

namespace App.Application.Validations;

public class EdicaoDecretoValidator: AbstractValidator<AtualizarDecretoDto>
{        
    private readonly IDecretoRepository _repository;

    public EdicaoDecretoValidator(IDecretoRepository repository)
    {
        _repository = repository;
        
        RuleFor(d => d.NumeroDecreto)
            .GreaterThan(0)
            .WithMessage("Número do decreto é obrigatório.")
            .MustAsync(async (numero, cancellation) =>
            {
                var ultimoNumero = await repository.BuscarUltimoDecreto();
                return numero == ultimoNumero + 1;
            })
            .WithMessage("Número do decreto deve seguir a sequência correta.");

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