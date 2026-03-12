using Worker.Notificacoes.Interfaces;

namespace Worker.Notificacoes.Services;

public class WhatsappService : IWhatsappService
{
    public Task EnviarWhatsAsync(string mensagem)
    {
        Console.WriteLine("Whatsapp enviado");
        Console.WriteLine($"Mensagem: {mensagem}");

        return Task.CompletedTask;
    }
}