using App.Application.Interfaces.Repository;
using App.Application.Validations;
using CammonTestsUtilies.Request;
using Moq;
using Shouldly;

namespace UnitTests.Usuario.Registro;

public class UsuarioValidatorTest
{
    [Fact]
    public async Task Succes()
    {
        var repositoryMock = new Mock<IUsuarioRepository>();

        repositoryMock
            .Setup(r => r.BuscarViaMatricula(It.IsAny<int>()))
            .ReturnsAsync((App.Domain.Usuario)null);

        repositoryMock
            .Setup(r => r.BuscarViaEmail(It.IsAny<string>()))
            .ReturnsAsync((App.Domain.Usuario)null);
        
        var validator = new UsuarioValidator(repositoryMock.Object);
        
        var request = CriarUsuarioJsonBuilder.Build();

        var result = await validator.ValidateAsync(request);

        result.ShouldNotBeNull();

    }
    
}