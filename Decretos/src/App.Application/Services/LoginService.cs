using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Domain.Exceptions;

namespace App.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUsuarioRepository _repository;
    private readonly IHashSenhaService _hashSenha;
    private readonly IJwtService _jwtService;

    public LoginService(IUsuarioRepository repository,IHashSenhaService hashSenha, IJwtService jwtService)
    
    {
        _repository = repository;
        _hashSenha = hashSenha;
        _jwtService = jwtService;
    }

    public async Task<TokenDto>Login(LoginDto dto)
    {
        var usuario = await _repository.BuscarViaEmail(dto.Email) ??
            throw new BusinessException("Email ou senha inválidos.");

        var senhaValida = _hashSenha.Verificar(dto.Senha, usuario.Senha);
        if(!senhaValida)
            throw new BusinessException("Email ou senha inválidos.");
        
        var token = _jwtService.GerarToken(usuario);

        return token;
    }
}