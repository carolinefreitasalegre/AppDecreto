using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.Messaging;

public class RabbitMqService : IRabbitMqService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublicarAsync<T>(T mensagem, string fila)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
            UserName = _configuration["RabbitMQ:Username"] ?? "guest",
            Password = _configuration["RabbitMQ:Password"] ?? "guest",
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: fila,
            durable: false,
            exclusive: false,
            autoDelete: false
        );

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(mensagem));

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: fila,
            body: body
        );
    }}