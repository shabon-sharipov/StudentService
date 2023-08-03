using RabbitMQ.Client;

namespace Application.Common.interfaces;

public class HelperClass
{
    public ConnectionFactory ConnectionFactory()
    {
        ConnectionFactory factory = new();
        factory.Uri = new Uri(Constant.BaseUrlRabbitMq);
        factory.ClientProvidedName = "Rabbit Sender App";

        return factory;
    }
}