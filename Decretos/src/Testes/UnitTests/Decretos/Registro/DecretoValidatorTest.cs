using App.Application.Validations;
using CammonTestsUtilies.Request;
using Shouldly;

namespace UnitTests.Decretos.Registro;

public class DecretoValidatorTest
{
    [Fact]
    public async Task Success()
    {
        var validator = new DecretoValidator();

        var request = CriarDecretoJsonBuilder.Build();

        var result = await validator.ValidateAsync(request);
        
        result.ShouldNotBeNull();
    }
}