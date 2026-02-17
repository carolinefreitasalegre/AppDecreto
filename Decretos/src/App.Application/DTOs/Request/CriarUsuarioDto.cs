using App.Application.DTOs;

namespace App.Application;

public class CriarUsuarioDto : UsuarioBaseDto
{
    public string Senha { get; set; }
}