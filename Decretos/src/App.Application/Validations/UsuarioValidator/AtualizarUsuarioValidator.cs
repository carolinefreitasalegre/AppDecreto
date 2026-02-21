using App.Application.Interfaces.Repository;
using FluentValidation;

namespace App.Application.Validations;

public class AtualizarUsuarioValidator : AbstractValidator<AtualizarUsuarioDto>
{
    public AtualizarUsuarioValidator(IUsuarioRepository repository)
    {
        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.")
            .MustAsync(async (dto, email, _) =>
            {
                var usuario = await repository.BuscarViaEmail(email);
                return usuario is null || usuario.Id == dto.Id;
            })
            .WithMessage("Email já está cadastrado.");

        RuleFor(u => u.Matricula)
            .GreaterThanOrEqualTo(10000)
            .WithMessage("Matrícula deve ter no mínimo 5 dígitos.")
            .MustAsync(async (dto, matricula, _) =>
            {
                var usuario = await repository.BuscarViaMatricula(matricula);
                return usuario is null || usuario.Id == dto.Id;
            })
            .WithMessage("Matrícula já está cadastrada.");

        RuleFor(u => u.Role)
            .IsInEnum().WithMessage("Role inválido.");

        RuleFor(u => u.Status)
            .IsInEnum().WithMessage("Status inválido.");
    }
}