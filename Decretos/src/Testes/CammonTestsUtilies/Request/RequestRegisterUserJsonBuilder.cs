using App.Application;
using App.Domain.Enums;
using Bogus;

namespace CammonTestsUtilies.Request;

public class RequestRegisterUserJsonBuilder
{
    public static CriarUsuarioDto Build()
    {
        return new Faker<CriarUsuarioDto>()
            .RuleFor(u => u.Nome, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Nome))
            .RuleFor(u => u.Senha, (f) => f.Internet.Password())
            .RuleFor(u => u.Matricula, (f) => f.Random.Int(1000, 9999))
            .RuleFor(u => u.Role, (f) => f.PickRandom<UsuarioRole>())
            .RuleFor(u => u.Status, (f) => f.PickRandom<UsuarioStatus>());
    }
}