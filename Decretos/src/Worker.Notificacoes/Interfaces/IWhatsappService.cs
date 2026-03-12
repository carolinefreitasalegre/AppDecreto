namespace Worker.Notificacoes.Interfaces;

public interface IWhatsappService
{
    Task EnviarWhatsAsync(string mensagem);
}