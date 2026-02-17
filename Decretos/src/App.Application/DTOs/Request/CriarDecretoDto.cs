using App.Application.DTOs;

namespace App.Application;

public class CriarDecretoDto : DecretoBaseDto
{
    public int UsuarioId { get; set; }
}