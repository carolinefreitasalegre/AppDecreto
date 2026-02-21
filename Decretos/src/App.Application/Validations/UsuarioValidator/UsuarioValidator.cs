using App.Application.Interfaces.Repository;
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
            .WithMessage("Matricula deve conter pelo menos 6 carácteres.")
            .MustAsync(async (matricula, cancellation) =>
            {
                var usuario = await repository.BuscarViaMatricula(matricula);
                return usuario is null;
            })
            .WithMessage("Matrícula já está cadastrada.");
          
        
        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.")
            .MinimumLength(3)
            .WithMessage("Nome deve ter pelo menos 3 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório.")
            .EmailAddress()
            .WithMessage("Email inválido.")
            .MustAsync(async (email, cancellation) =>
            {
                var usuario = await repository.BuscarViaEmail(email);
                return usuario is null; 
            })
            .WithMessage("Email já está cadastrado.");

        RuleFor(u => u.Role)
            .IsInEnum()
            .WithMessage("Role inválido.");

        RuleFor(u => u.Status)
            .IsInEnum()
            .WithMessage("Status inválido.");

        RuleFor(u => u.Senha)
            .NotEmpty()
            .WithMessage("Senha é obrigatória.")
            .MinimumLength(6)
            .WithMessage("Senha deve ter pelo menos 6 caracteres.")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$")
            .WithMessage("Senha deve conter letras e números.");
    }
}