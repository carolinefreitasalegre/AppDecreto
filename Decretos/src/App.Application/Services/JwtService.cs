using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Application.Interfaces;
using App.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public TokenDto GerarToken(Usuario usuario)
    {
        var key = _config["Jwt:Key"]!;
        var issuer = _config["JwtIssuer"]!;
        var audience = _config["Jwt:Audience"]!;
        var expireInHours = int.Parse(_config["Jwt:ExpireInHours"]!);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Role.ToString()),
            new Claim("matricula", usuario.Matricula.ToString())
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience:audience,
            claims:claims,
            expires:DateTime.UtcNow.AddHours(expireInHours),
            signingCredentials: credentials
            );
        
        return new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = DateTime.UtcNow.AddHours(expireInHours),
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role.ToString()
        };
    }
}