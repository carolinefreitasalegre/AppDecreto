namespace App.Application.Interfaces;

public interface ILoginService
{
    Task<TokenDto> Login(LoginDto dto);
}