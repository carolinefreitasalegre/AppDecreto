using App.Application.DTOs;

namespace App.Application;

public class UsuariosDto : UsuarioBaseDto
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; }
}