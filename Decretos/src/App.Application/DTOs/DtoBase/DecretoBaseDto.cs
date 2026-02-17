using App.Domain.Enums;

namespace App.Application.DTOs;

public abstract class DecretoBaseDto
{
    public int NumeroDecreto { get; set; }
    public string Solicitante { get; set; } = null!;
    public DateTime DataSolicitacao { get; set; }
    public DateTime DataParaUso { get; set; }
    public Secretaria Secretaria { get; set; }
    public string Justificativa { get; set; } = null!;
}
