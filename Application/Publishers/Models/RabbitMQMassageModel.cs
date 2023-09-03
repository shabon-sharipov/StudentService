namespace Application.Consumer.Models;

public class RabbitMQMassageModel
{
    public string Id { get; set; }
    public string Type { get; set; }
    public EntityChangeEventType EntityChangeEventType { get; set; }
}