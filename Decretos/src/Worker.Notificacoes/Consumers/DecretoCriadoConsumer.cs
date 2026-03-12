using App.Application.Events;
using Worker.Notificacoes.Interfaces;

namespace Worker.Notificacoes.Consumers;

public class DecretoCriadoConsumer
{
    private readonly IEmailService _emailService;
    private readonly IWhatsappService _whatsappService;

    public DecretoCriadoConsumer(
        IEmailService emailService,
        IWhatsappService whatsappService)
    {
        _emailService = emailService;
        _whatsappService = whatsappService;
    }

    public async Task ProcessarAsync(DecretoCriadoEvent evento)
    {
        var mensagem = $"Novo decreto criado: {evento.NumeroDecreto}";

        await _emailService.EnviarEmailAsync(
            "Novo decreto publicado",
            mensagem
        );
        
        await _whatsappService.EnviarWhatsAsync(mensagem);
    }
}