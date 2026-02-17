using App.Application.Interfaces;
using BCrypt.Net;


namespace App.Application.Services;

public class HashSenhaService : IHashSenhaService
{
    public string GerarHash(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool Verificar(string senha, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}