using App.Application.Interfaces.Repository;
using Exceptions.Exceptions;
using FluentValidation;

namespace App.Application.Validations;

public class EdicaoDecretoValidator: AbstractValidator<AtualizarDecretoDto>
{        
    private readonly IDecretoRepository _repository;

    public EdicaoDecretoValidator(IDecretoRepository repository)
    {
        _repository = repository;

        RuleFor(d => d.Solicitante)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.DECRETO_SOLICITANTE_EMPTY)
            .MinimumLength(5)
            .WithMessage(ResourceMessagesExceptions.DECRETO_SOLICITANTE_MIN_CHARACTER);

        RuleFor(d => d.DataParaUso)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage(ResourceMessagesExceptions.DECRETO_DATA_INVALID);

        RuleFor(d => d.Secretaria)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.DECRETO_SECRETARIA_INVALID);

        RuleFor(d => d.Justificativa)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.DECRETO_JUSTIFICATIVA_EMPTY);
    }

}