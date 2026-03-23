namespace App.Domain;

public class HistoricoDecreto
{
    public int Id { get; set; }
    public int DecretoId { get; set; }
    public Decreto Decreto { get; set; }
    public string CampoAlterado { get; set; }
    public string ValorAnterior { get; set; }
    public string  ValorNovo { get; set; }
    public DateTime DataAlteracao { get; set; }
    public Usuario Usuario { get; set; }
    public int UsuarioId { get; set; }
    public string Justificativa { get; private set; } = null!;  

}