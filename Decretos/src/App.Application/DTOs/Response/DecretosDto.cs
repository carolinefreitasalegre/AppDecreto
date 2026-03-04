using App.Application.DTOs;

namespace App.Application;

public class DecretosDto : DecretoResponseBaseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
}