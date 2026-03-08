using App.Application.Interfaces.Repository;
using Exceptions.Exceptions;
using FluentValidation;

namespace App.Application.Validations;

public class AtualizarUsuarioValidator : AbstractValidator<AtualizarUsuarioDto>
{
    public AtualizarUsuarioValidator(IUsuarioRepository repository)
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.NAME_EMPTY)
            .MinimumLength(3)
            .WithMessage(ResourceMessagesExceptions.MINIMUM_CHARACTER);

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceMessagesExceptions.EMAIL_INVALID)
            .MustAsync(async (dto, email, _) =>
            {
                var usuario = await repository.BuscarViaEmail(email);
                return usuario is null || usuario.Id == dto.Id;
            })
            .WithMessage(ResourceMessagesExceptions.EMAIL_CADASTRADO);

        RuleFor(u => u.Matricula)
            .GreaterThanOrEqualTo(10000)
            .WithMessage(ResourceMessagesExceptions.MINIMAL_REGISTRATION_CHARACTER)
            .MustAsync(async (dto, matricula, _) =>
            {
                var usuario = await repository.BuscarViaMatricula(matricula);
                return usuario is null || usuario.Id == dto.Id;
            })
            .WithMessage(ResourceMessagesExceptions.REGISTRATION_ALREADY_REGISTERED);

        RuleFor(u => u.Role)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.INVALID_ROLE);

        RuleFor(u => u.Status)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.INVALID_STATUS);
    }
}