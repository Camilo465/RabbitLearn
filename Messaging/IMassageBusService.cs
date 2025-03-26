using LearnRabbitMQ;

namespace Messaging;
public interface IMassageBusService
{
    Task Publish(WeatherForecast[] weatherForecast);
}