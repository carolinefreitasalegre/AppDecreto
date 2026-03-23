using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Worker.Notificacoes.Interfaces;

namespace Worker.Notificacoes.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }
    
    public async Task EnviarEmailAsync(string destinatario, string assunto, string mensagem)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["Email:Remetente"]));
        email.To.Add(MailboxAddress.Parse(destinatario));
        email.Subject = assunto;
        email.Body = new TextPart("plain") { Text = mensagem };
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = mensagem
        };
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _config["Email:Host"],
            int.Parse(_config["Email:Porta"]!),
            MailKit.Security.SecureSocketOptions.StartTls);
            
        await smtp.AuthenticateAsync(
            _config["Email:Usuario"],
            _config["Email:Senha"]);
        
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("E-mail enviado para {Dest}", destinatario);

    }

}