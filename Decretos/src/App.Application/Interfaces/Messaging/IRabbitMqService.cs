namespace App.Infrastructure.Messaging;

public interface IRabbitMqService
{
    Task PublicarAsync<T>(T mensagem, string fila);
}