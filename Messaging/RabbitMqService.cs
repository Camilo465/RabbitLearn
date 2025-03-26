using System.Text;
using LearnRabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Messaging;

public class RabbitMqService : IMassageBusService
{
    public void Publish(WeatherForecast[] weatherForecast)
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMQConfiguration.HostName,
            UserName = RabbitMQConfiguration.UserName,
            Password = RabbitMQConfiguration.Password
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "test_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        
        string logContent = JsonConvert.SerializeObject(weatherForecast);
        var body = Encoding.UTF8.GetBytes(logContent);

        channel.BasicPublish(exchange: "",
                                routingKey: "test_queue",
                                basicProperties: null,
                                body: body);

        Console.WriteLine($"[X] Enviada: {logContent}");
    }
}
