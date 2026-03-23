using MassTransit;
using Worker.Notificacoes;
using Worker.Notificacoes.Consumers;
using Worker.Notificacoes.Interfaces;
using Worker.Notificacoes.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker.Notificacoes.Worker>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<DecretoCriadoConsumer>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DecretoCriadoConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

var host = builder.Build();
host.Run();
