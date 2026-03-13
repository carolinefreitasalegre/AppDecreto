using System.Security.Claims;
using App.Domain;

namespace App.Application.Interfaces;

public interface IJwtService
{
    TokenDto GerarToken(Usuario usuario);
}