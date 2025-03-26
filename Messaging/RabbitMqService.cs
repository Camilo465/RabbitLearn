using System.Text;
using LearnRabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Messaging;

public class RabbitMqService : IMassageBusService
{
    public async Task Publish(WeatherForecast[] weatherForecast)
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMQConfiguration.HostName,
            UserName = RabbitMQConfiguration.UserName,
            Password = RabbitMQConfiguration.Password
        };

        using var connection = await Task.Run(() => factory.CreateConnection());
        using var channel = await Task.Run(() => connection.CreateModel());

        channel.QueueDeclare(queue: "test_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        
        string logContent = JsonConvert.SerializeObject(weatherForecast);
        var body = Encoding.UTF8.GetBytes(logContent);

        await Task.Run(() => channel.BasicPublish(exchange: "",
                                                  routingKey: "test_queue",
                                                  basicProperties: null,
                                                  body: body));

        Console.WriteLine($"Enviada: {logContent}");
    }
}
