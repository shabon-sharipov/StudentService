namespace Application.Options;

public class RabbitMQOptions
{
    public string Url { get; set; }
    public string ExchangeName { get; set; }
    public string RoutinKey { get; set; }
    public string QueueName { get; set; }
}
