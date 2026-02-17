namespace App.Application.Interfaces;

public interface IHashSenhaService
{
    string GerarHash(string senha);
    bool Verificar(string senha, string hash);
}