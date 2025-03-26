using LearnRabbitMQ;

namespace Messaging;
public interface IMassageBusService
{
    void Publish(WeatherForecast[] weatherForecast);
}