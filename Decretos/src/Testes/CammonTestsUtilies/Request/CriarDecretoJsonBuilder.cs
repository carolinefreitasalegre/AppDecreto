using App.Application;
using App.Domain.Enums;
using Bogus;

namespace CammonTestsUtilies.Request;

public class CriarDecretoJsonBuilder
{
    public static CriarDecretoDto Build()
    {
        return new Faker<CriarDecretoDto>()
            .RuleFor(d => d.Solicitante, (f) => f.Person.FirstName)
            .RuleFor(d=> d.DataParaUso, (f) => f.Date.Future())
            .RuleFor(d=>d.Secretaria, (f) => f.PickRandom<Secretaria>())
            .RuleFor(d=>d.Justificativa, (f) => f.Lorem.Sentence());
    }
}