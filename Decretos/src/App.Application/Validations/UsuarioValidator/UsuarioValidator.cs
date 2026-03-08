using App.Application.Interfaces.Repository;
using Exceptions.Exceptions;
using FluentValidation;

namespace App.Application.Validations;

public class UsuarioValidator : AbstractValidator<CriarUsuarioDto>
{        
    private readonly IUsuarioRepository _repository;
    
    public UsuarioValidator(IUsuarioRepository repository)
    {
        _repository = repository;

        RuleFor(u => u.Matricula)
            .GreaterThan(0)
            .WithMessage(ResourceMessagesExceptions.MINIMAL_REGISTRATION_CHARACTER)
            .MustAsync(async (matricula, cancellation) =>
            {
                var usuario = await repository.BuscarViaMatricula(matricula);
                return usuario is null;
            })
            .WithMessage(ResourceMessagesExceptions.REGISTRATION_ALREADY_REGISTERED);
          
        
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
            .MustAsync(async (email, cancellation) =>
            {
                var usuario = await repository.BuscarViaEmail(email);
                return usuario is null; 
            })
            .WithMessage(ResourceMessagesExceptions.EMAIL_CADASTRADO);

        RuleFor(u => u.Role)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.INVALID_ROLE);

        RuleFor(u => u.Status)
            .IsInEnum()
            // .WithMessage("Status inválido.");
            .WithMessage(ResourceMessagesExceptions.INVALID_STATUS);

        RuleFor(u => u.Senha)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.PASS_EMPTY)
            .MinimumLength(6)
            .WithMessage(ResourceMessagesExceptions.PASS_MINIMAL_CHARACTER)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$")
            .WithMessage(ResourceMessagesExceptions.PASS_CHARACTER_ESPETIAL);
    }
}