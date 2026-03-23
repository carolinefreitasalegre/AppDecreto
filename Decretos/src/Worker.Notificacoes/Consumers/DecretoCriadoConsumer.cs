using App.Application.Events;
using MassTransit;
using Worker.Notificacoes.Interfaces;

namespace Worker.Notificacoes.Consumers;

public class DecretoCriadoConsumer : IConsumer<DecretoCriadoEvent>
{
    private readonly IEmailService _service;
    private readonly ILogger<DecretoCriadoConsumer> _logger;

    public DecretoCriadoConsumer(IEmailService service,ILogger<DecretoCriadoConsumer> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<DecretoCriadoEvent> context)
    {
        var evento = context.Message;
        _logger.LogInformation("Decreto recebido: {Numero}", evento.NumeroDecreto);

        var destinatarios = new[] { "freitascaroline.92@gmail.com" };
        
        foreach(var email in destinatarios)
        {
            await _service.EnviarEmailAsync(
                email,
                $"Novo Decreto Solicitado: nº {evento.NumeroDecreto}",
                $"O Decreto nº <strong>{evento.NumeroDecreto}</strong> foi solicitado com data para uso de: <strong>{evento.DataParaUso:dd/MM/yyyy}</strong>");
        }
    }
}