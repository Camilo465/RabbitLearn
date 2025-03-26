namespace Messaging;
public static class RabbitMQExtension
{
    public static void SeedRabbitMQConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        RabbitMQConfiguration.HostName = configuration["RabbitMQConfiguration:HostName"] ?? "";
        RabbitMQConfiguration.UserName = configuration["RabbitMQConfiguration:UserName"] ?? "";
        RabbitMQConfiguration.Password = configuration["RabbitMQConfiguration:Password"] ?? "";
    }
}