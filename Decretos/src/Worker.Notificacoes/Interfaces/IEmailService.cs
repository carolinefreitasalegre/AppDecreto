namespace Worker.Notificacoes.Interfaces;

public interface IEmailService
{
    Task EnviarEmailAsync(string assunto, string mensagem);
}