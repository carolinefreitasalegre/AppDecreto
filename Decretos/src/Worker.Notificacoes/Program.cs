using Worker.Notificacoes;
using Worker.Notificacoes.Consumers;
using Worker.Notificacoes.Interfaces;
using Worker.Notificacoes.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker.Notificacoes.Worker>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IWhatsappService, WhatsappService>();
builder.Services.AddScoped<DecretoCriadoConsumer>();
var host = builder.Build();
host.Run();
