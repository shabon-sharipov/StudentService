using Microsoft.Extensions.Configuration;

namespace Application.Consumer;

public enum EntityChangeEventType
{
    Insert,
    Update,
    Delete
}

public class StudentPublisher : IStudentPublisher
{
    private readonly RabbitMQOptions _options;
    private readonly ConnectionFactory _factory;

    public StudentPublisher(IConfiguration configuration)
    {
        _options = configuration.GetSection("RabbitMQ")?.Get<RabbitMQOptions>() ?? throw new ArgumentNullException("RabbitMQ");

        _factory = new ConnectionFactory()
        {
            Uri = new Uri(_options.Url),
            ClientProvidedName = "Rabbit Sender App"
        };
        Console.WriteLine(_options.Url);
    }

    public void SendInsert(string id) => Send(id, EntityChangeEventType.Insert);
    public void SendUpdated(string id) => Send(id, EntityChangeEventType.Update);
    public void SendDeleted(string id) => Send(id, EntityChangeEventType.Delete);

    void Send(string id, EntityChangeEventType entityChangeEventType)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(_options.ExchangeName, ExchangeType.Direct);
        channel.QueueDeclare(_options.QueueName, false, false, false, null);
        channel.QueueBind(_options.QueueName, _options.ExchangeName, _options.RoutinKey, null);

        var serializedModel = JsonSerializer.Serialize(new RabbitMQMassageModel
        { Id = id.ToString(), Type = nameof(Student), EntityChangeEventType = entityChangeEventType });

        var message = Encoding.UTF8.GetBytes(serializedModel);
        try
        {
            channel.BasicPublish(_options.ExchangeName, _options.RoutinKey, null, message);
        }
        catch (Exception e)
        {
            throw new Exception($"When Publish data to RabbitMq: {e.Message}");
        }
        finally
        {
            channel.Close();
            connection.Close();
        }
    }
}