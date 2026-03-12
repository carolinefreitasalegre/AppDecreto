namespace App.Application.Events;

public class DecretoCriadoEvent
{
    public int NumeroDecreto { get; set; }
    public string Solicitante { get; set; }
    public DateTime DataParaUso { get; set; }
}