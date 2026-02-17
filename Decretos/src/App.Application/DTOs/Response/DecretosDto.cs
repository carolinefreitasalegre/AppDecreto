using App.Application.DTOs;

namespace App.Application;

public class DecretosDto : DecretoBaseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime CriadoEm { get; set; }
}