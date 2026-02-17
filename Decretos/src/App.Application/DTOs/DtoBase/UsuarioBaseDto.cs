using App.Domain.Enums;

namespace App.Application.DTOs;

public abstract class UsuarioBaseDto
{
    public int Matricula { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UsuarioRole Role { get; set; }
    public UsuarioStatus Status { get; set; } 
}