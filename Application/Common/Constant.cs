namespace Application.Common.interfaces;

public class Constant
{
    //RabbitMq
    public const string BaseUrlRabbitMq = "amqp://guest:guest@localhost:5672";
    public const string ExchangeName = "DemoExchange";
    public const string RoutinKey = "demo-routin-key";
    public const string QueueName = "DemoQueue";
}