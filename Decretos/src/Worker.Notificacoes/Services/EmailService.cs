using Worker.Notificacoes.Interfaces;

namespace Worker.Notificacoes.Services;

public class EmailService : IEmailService
{
    public Task EnviarEmailAsync(string assunto, string mensagem)
    {
        Console.WriteLine("EMAIL ENVIADO");
        Console.WriteLine($"Assunto: {assunto}");
        Console.WriteLine($"Mensagem: {mensagem}");

        return Task.CompletedTask;
    }
}